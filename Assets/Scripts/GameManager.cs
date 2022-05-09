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
    private PlayerInput touchControls;
    private float timer;

    public TextMeshProUGUI scrapCountText;
    public TextMeshProUGUI goldCountText;

    public int factoryTier;


    //lists of in-scene Fabricators and Conveyors
    public List<GameObject> Fabricators;
    public List<GameObject> Conveyors;


    //variables for saving and loading data
    private float saveTime;
    private float loadTime;
    private PlayerData save;

    //current cash variable
    public float goldCount;
    public float scrapCount;

    //variables for cost of items
    private float producedCount;
    private float robotCost;

    //variables for scrap upgrades
    public float scrapCap;
    public float scrapRecharge;

    //variables for conveyor upgrades
    public float conveyorSpeed;

    //variables for truck upgrades
    public float truckCap;
    public float truckSpeed;

    //variables for fabricator upgrades
    public float fabricatorSpeed;

    //variables for robot value upgrades
    public float robotValue;


    private void Start()
    {
        touchControls = new PlayerInput();
        touchControls.Enable();
        StartValues();

        if (PlayerPrefs.HasKey("SaveCheck"))
        {
            LoadData();
        }

        StartCoroutine(IncrementScrap());
        StartCoroutine(StartUIUpdate());
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


    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + (int)scrapCount;
        goldCountText.text = goldCount.ToString();
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




    private void StartValues()
    {
        factoryTier = 1;
        goldCount = 0f;
        producedCount = 0f;
        robotCost = 10f;

        scrapCap = 100f;
        scrapCount = 50f;
        scrapRecharge = 5F;

        foreach (GameObject conveyor in GameManager.instance.Conveyors)
        {
            conveyor.GetComponent<Conveyor>().speed = 0.5f;
        }

        truckCap = 6f;
        truckSpeed = 1f;

        fabricatorSpeed = 1f;

        robotValue = 25f;

        //SceneManager.LoadScene(factoryTier);
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


    public void ProduceRobot()
    {
        if (scrapCount >= robotCost)
        {
            producedCount++;
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
        conveyorSpeed = PlayerPrefs.GetFloat("conveyorTier");
        fabricatorSpeed = PlayerPrefs.GetFloat("fabricatorTier");
        robotValue = PlayerPrefs.GetFloat("robotTier");
        loadTime = Time.time;
        int loadResources = (int)loadTime - (int)saveTime;
        if (loadResources > 20f)
            goldCount = (loadResources - 20f) * robotValue;

        //SceneManager.LoadScene(factoryTier);
    }


    private void SaveData()
    {
        /*
    public float scrapCount;
    public float gold;
    public float saveTime;
    public float factoryTier;
    public float conveyorTier;
    public float fabricatorTier;
    public float robotTier;
    public float scrapCapTier;
    public float scrapRechargeTier;
        save.scrapCount = scrapCount;
        save.cashCount = cashCount;
        save.saveTime = Time.time;
        SaveManager.instance.SaveGame(save);
        */
        PlayerPrefs.SetInt("SaveCheck", 1);
        PlayerPrefs.SetFloat("scrapCapTier", scrapCap);
        PlayerPrefs.SetFloat("scrapRechargeTier", scrapRecharge);
        PlayerPrefs.SetFloat("goldCountTier", goldCount);
        PlayerPrefs.SetFloat("saveTime", saveTime);
        PlayerPrefs.SetInt("factoryTier", factoryTier);
        PlayerPrefs.SetFloat("conveyorTier", conveyorSpeed);
        PlayerPrefs.SetFloat("fabricatorTier", fabricatorSpeed);
        PlayerPrefs.SetFloat("robotTier", robotValue);
        PlayerPrefs.Save();
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


}
