using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;


public class GameManager : Singleton<GameManager>
{
    [Header("Gold required for next factory")]
    public float goldRequiredForFactory;
    public GameObject factoryUpgradeButton;


    private PlayerInput touchControls;
    private float timer;

    public TextMeshProUGUI scrapCountText;
    public TextMeshProUGUI goldCountText;

    public int factoryTier;
    private bool goldReached;

    //lists of in-scene Fabricators and Conveyors
    public List<GameObject> Fabricators;
    public List<GameObject> Conveyors;


    //variables for conveyor upgrades
    public List<float> speeds;
    public int curConveyorTier;
    public int conveyorTier;


    //variables for saving and loading data
    private float saveTime;
    private float loadTime;

    //current cash variable
    public float goldCount = 0;
    public float goldTemp = 0;
    public float scrapCount = 50;

    //variables for cost of items
    private float robotCost = 10f;

    //variables for scrap upgrades
    public float scrapCap = 100f;
    public float scrapRecharge = 5f;



    //variables for truck upgrades
    public float truckCap;
    public float truckSpeed;

    //variables for fabricator upgrades
    public float fabricatorSpeed;

    //variables for robot value upgrades
    public float robotValue;


    private void Start()
    {
        factoryUpgradeButton = GameObject.FindGameObjectWithTag("FactoryButton");
        goldReached = false;
        IsDestroyedOnLoad = true;
        factoryTier = 1;
        curConveyorTier = 0;
        factoryUpgradeButton.gameObject.SetActive(false);

        touchControls = new PlayerInput();
        touchControls.Enable();
        speeds = new List<float>();
        StartValues();
        if (PlayerPrefs.HasKey("SaveCheck"))
        {
            LoadData();
        }

        StartCoroutine(IncrementScrap());
        StartCoroutine(StartUIUpdate());
        StartCoroutine(GoldCheck());
        GetConveyors();
        GetFabricators();
        
    }


    IEnumerator StartUIUpdate()
    {
        while (true)
        {
            UpdateUI();
            SaveData();
            yield return new WaitForSeconds(0.5f);
        }
    }



    IEnumerator GoldCheck()
    {
        while (!goldReached)
        {
            yield return new WaitForSeconds(10f);
            if(goldCount > goldRequiredForFactory)
            {
                goldReached = true;
                factoryUpgradeButton.gameObject.SetActive(true);
            }
        }
       
    }



    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + (int)scrapCount;
        goldTemp = (int)goldCount;
        goldCountText.text = goldTemp.ToString();
    }


    IEnumerator IncrementScrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (scrapCap - scrapCount < scrapRecharge)
            {
                scrapCount = scrapCap;
            }
            else
                scrapCount += scrapRecharge;
        }
    }


    public void UpgradeFactory()
    {
        factoryTier++;
        StartValues();
        SaveData();
        StartCoroutine(SwapScene());
    }
    

    IEnumerator SwapScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(factoryTier);


    }

    private void StartValues()
    {
        goldCount = 0f;
        goldTemp = 0f;
        robotCost = 10f;
        scrapCap = 100f;
        scrapCount = 50f;
        scrapRecharge = 5F;
        goldReached = false;

        foreach (GameObject conveyor in GameManager.instance.Conveyors)
        {
            conveyor.GetComponent<Conveyor>().speed = 0.5f;
        }

        truckCap = 6f;
        truckSpeed = 1f;

        fabricatorSpeed = 1f;

        robotValue = 25f;

    }


    private void GetConveyors()
    {
        Conveyors = new List<GameObject>();
        Conveyors = GameObject.FindGameObjectsWithTag("Conveyor").ToList();
    }

    private void GetFabricators()
    {
        Fabricators = new List<GameObject>();
        Fabricators = GameObject.FindGameObjectsWithTag("Fabricator").ToList();
    }


    public void SetConveyorSpeeds(float speedToAdd)
    {
        speeds.Add(speedToAdd);
    }



    public void ApplyConveyorSpeeds()
    {
        foreach (GameObject conveyor in Conveyors)
        {
            conveyor.GetComponent<Conveyor>().speed = speeds[curConveyorTier - 1];
        }
    }


    public void UpgradeConveyor()
    {
        if(conveyorTier < speeds.Count)
        {
            conveyorTier++;
            curConveyorTier = conveyorTier;
            ApplyConveyorSpeeds();
        }
    }



    public void ProduceRobot()
    {
        if (scrapCount >= robotCost)
        {
            scrapCount -= robotCost;
            UpdateUI();
            foreach (GameObject fabricator in Fabricators)
            {
                if (fabricator.GetComponent<Fabricator>().processNumber == 1)
                    fabricator.GetComponent<Fabricator>().buildRobot();
            }
        }
        //get reference to Fabricator Object
        //spawn robo part 1 and add to list
        //apply force to push onto conveyor from spawn

    }

    public void addCash()
    {
        goldCount += 5 + robotValue;
        UpdateUI();
    }




    public bool spendCash(float cost)
    {
        if (cost <= goldCount)
        {
            goldCount -= cost;
            return true;
        }
        else
            return false;
    }



    private void LoadData()
    {
        factoryTier = PlayerPrefs.GetInt("factoryTier");

        scrapCap = PlayerPrefs.GetFloat("scrapCapTier");
        scrapRecharge = PlayerPrefs.GetFloat("scrapRechargeTier");
        goldCount = PlayerPrefs.GetFloat("goldCount");
        saveTime = PlayerPrefs.GetFloat("saveTime");
        conveyorTier = PlayerPrefs.GetInt("conveyorTier");
        fabricatorSpeed = PlayerPrefs.GetFloat("fabricatorTier");
        robotValue = PlayerPrefs.GetFloat("robotValue");
        loadTime = Time.time;
        int loadResources = (int)loadTime - (int)saveTime;
        if (loadResources > 20f)
            goldCount = (loadResources - 20f) * robotValue;

        //SceneManager.LoadScene(factoryTier);
    }



    private void SaveData()
    {
   
        PlayerPrefs.SetInt("SaveCheck", 1);
        PlayerPrefs.SetFloat("scrapCapTier", scrapCap);
        PlayerPrefs.SetFloat("scrapRechargeTier", scrapRecharge);
        PlayerPrefs.SetFloat("goldCountTier", goldCount);
        PlayerPrefs.SetFloat("saveTime", saveTime);
        PlayerPrefs.SetInt("factoryTier", factoryTier);
        PlayerPrefs.SetInt("conveyorTier", conveyorTier);
        PlayerPrefs.SetFloat("fabricatorTier", fabricatorSpeed);
        PlayerPrefs.SetFloat("robotValue", robotValue);
        PlayerPrefs.Save();
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


}
