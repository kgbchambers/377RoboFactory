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
    public float goldCount;
    public float goldTemp;
    public float scrapCount;

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
        
        factoryTier = 1;

        factoryUpgradeButton = GameObject.FindGameObjectWithTag("FactoryButton");
        goldReached = false;
        IsDestroyedOnLoad = true;

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
        LoadData();
        //Debug.Log(conveyorTier);
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
        if(goldCount == 0 && scrapCount == 0)
            {
                if (PlayerPrefs.HasKey("SaveCheck"))
                {
                    LoadData();
                }
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
        goldCount = 0f;
        goldTemp = 0f;
        robotCost = 10f;
        scrapCap = 100f;
        scrapCount = 50f;
        scrapRecharge = 5F;
        goldReached = false;
        SaveData();
        StartCoroutine(SwapScene());
    }
    

    IEnumerator SwapScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(factoryTier);
    }

    public void StartValues()
    {
        goldCount = 0f;
        goldTemp = 0f;
        robotCost = 10f;
        scrapCap = 100f;
        scrapCount = 50f;
        scrapRecharge = 5F;
        goldReached = false;
        conveyorTier = 0;

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
            conveyor.GetComponent<Conveyor>().speed = speeds[conveyorTier];
        }
        
    }


    public void UpgradeConveyor()
    {
        if(conveyorTier < speeds.Count - 1)
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



    public void LoadData()
    {
        factoryTier = PlayerPrefs.GetInt("factoryTier");

        scrapCap = PlayerPrefs.GetFloat("scrapCap");
        scrapRecharge = PlayerPrefs.GetFloat("scrapRecharge");
        goldCount = PlayerPrefs.GetFloat("goldCount");
        saveTime = PlayerPrefs.GetFloat("saveTime");
        conveyorTier = PlayerPrefs.GetInt("conveyorTier");
        fabricatorSpeed = PlayerPrefs.GetFloat("fabricatorTier");
        robotValue = PlayerPrefs.GetFloat("robotValue");
        loadTime = Time.time;
       //float loadResources = loadTime - saveTime;
   
       // if (loadResources > 20f)
       //     goldCount = (loadResources - 20f) * robotValue;

    }



    private void SaveData()
    {
   
        PlayerPrefs.SetInt("SaveCheck", 1);
        PlayerPrefs.SetFloat("scrapCap", scrapCap);
        PlayerPrefs.SetFloat("scrapRecharge", scrapRecharge);
        PlayerPrefs.SetFloat("goldCount", goldCount);
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
