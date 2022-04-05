using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    //public float conveyorSpeed;

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
        StartCoroutine(IncrementScrap());
        StartCoroutine(StartUIUpdate());
    }


    IEnumerator StartUIUpdate()
    {
        while (true)
        {
            UpdateUI();
            yield return new WaitForSeconds(0.5f);
        }
    }


    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + scrapCount;
        goldCountText.text = goldCount + " Gold";
    }


    IEnumerator IncrementScrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(scrapCap - scrapCount < scrapRecharge)
            {
                scrapCount = scrapCap;
            }
            else
                scrapCount += scrapRecharge;
        }
    }



    private void StartValues()
    {
        goldCount = 0f;
        producedCount = 0f;
        robotCost = 10f;

        scrapCap = 100f;
        scrapCount = scrapCap;
        scrapRecharge = 5F;

        foreach (GameObject conveyor in GameManager.instance.Conveyors)
        {
            conveyor.GetComponent<Conveyor>().speed = 0.1f;
        }

        truckCap = 6f;
        truckSpeed = 1f;

        fabricatorSpeed = 1f;

        robotValue = 25f;
}

    public void ProduceRobot()
    {
        if (scrapCount >= robotCost)
        {
            producedCount++;
            scrapCount -= robotCost;
            UpdateUI();
            foreach(GameObject fabricator in Fabricators)
            {
                if(fabricator.GetComponent<Fabricator>().processNumber == 1)
                    fabricator.GetComponent<Fabricator>().buildRobot();
            }
        }
        //get reference to Fabricator Object
        //spawn robo part 1 and add to list
        //apply force to push onto conveyor from spawn

    }

    public void addCash()
    {
        goldCount += 10.0f + robotValue;
        UpdateUI();
    }



    public void spendCash(float cost)
    {
        goldCount -= cost;
    }


    /*
    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + scrapCount;
        //countText.text = "Robots Produced: " + producedCount;
        cashCountText.text = "Cash: " + cashCount;
        robotCostText.text = "Required Scrap: " + robotCost;
    }

    
    private void ReloadData(PlayerData save)
    {
        producedCount = save.producedCount;
        scrapCount = save.scrapCount;
        cashCount = save.cashCount;
        loadTime = save.saveTime;
        int loadResources = ((int)loadTime - (int)Time.time) / 3;
        producedCount += loadResources;
    }

    private void SaveData(PlayerData save)
    {
        save.producedCount = producedCount;
        save.scrapCount = scrapCount;
        save.cashCount = cashCount;
        save.saveTime = Time.time;
        SaveManager.instance.SaveGame(save);
    }
    


    public void UpgradeScrap(float level, float cost)
    {
        if (cashCount >= cost)
        {
            scrapModifier = scrapModifier + level;
            cashCount -= cost;
        }
    }
    

    public void UpgradeCash(float level, float cost)
    {
        if(cashCount >= cost)
        {
            cashModifier += level * 5;
            cashCount -= cost;
            robotCost = robotCost + (level * 2); 
        }
    }


    public void UpgradeFabs(float level, float cost)
    { 
        if(cashCount >= cost)
        {
            cashCount -= cost;
            foreach(GameObject fab in Fabricators)
            {
                fab.GetComponent<Fabricator>().spawnPower += level;
            }
        }
    
    }


    public void UpgradeConveyors(float level, float cost)
    {
        if (cashCount >= cost)
        {
            cashCount -= cost;
            foreach (GameObject conveyor in Conveyors)
            {
                conveyor.GetComponent<Conveyor>().speed += level;
            }
        }

    }
    */



    private void OnApplicationQuit()
    {
        //SaveData(save);
    }

}
