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


    public TextMeshProUGUI countText;
    public TextMeshProUGUI scrapCountText;
    public TextMeshProUGUI cashCountText;
    public TextMeshProUGUI robotCostText;


    public List<GameObject> Fabricators;
    public List<GameObject> Conveyors;


    private PlayerData save;
    private float producedCount;
    private float robotCost;


    public float scrapCount;
    public float cashCount;
    private float saveTime;
    private float loadTime;
    public float scrapModifier;
    public float cashModifier;



    private void Start()
    {
        //touchControls = new PlayerInput();
        //touchControls.Enable();
        robotCost = 10f;
        scrapCount = 10f;
        save = SaveManager.instance.LoadGame();
        ReloadData(save);
        UpdateUI();
    }


    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            IncrementScrap();
            timer -= 1f;
        }
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }


    public void ProduceRobot()
    {
        if (scrapCount >= robotCost)
        {
            producedCount++;
            scrapCount -= robotCost;
            Fabricators[0].GetComponent<Fabricator>().buildRobot();
            UpdateUI();
        }
        //get reference to Fabricator Object
        //spawn robo part 1 and add to list
        //apply force to push onto conveyor from spawn

    }



    private void IncrementScrap()
    {
        scrapCount += 1 + scrapModifier * 2.0f;
    }
    
    private void IncrementScrap(int robots)
    {
        scrapCount = scrapCount + (robots * (1 + scrapModifier * 2.0f));
    }


    public void addCash()
    {
        cashCount += 10.0f + cashModifier;
    }
    public void addCash(int robots)
    {
        cashCount = cashCount + (robots * (10.0f + cashModifier));
    }



    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + scrapCount;
        countText.text = "Robots Produced: " + producedCount;
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
        addCash(loadResources);
        IncrementScrap(loadResources);
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




    private void OnApplicationQuit()
    {
        SaveData(save);
    }

}
