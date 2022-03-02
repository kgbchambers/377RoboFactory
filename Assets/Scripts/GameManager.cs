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

    private PlayerData save;
    private float producedCount;
    private float scrapCount;
    private float cashCount;
    private float saveTime;
    private float loadTime;

    private float scrapModifier;
    private float cashModifier;

    //private List<Parts> roboParts;



    private void Start()
    {
        touchControls = new PlayerInput();
        touchControls.Enable();
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
            UpdateUI();
            timer -= 1f;
        }
    }



    public void ProduceRobot()
    {
        if (scrapCount >= 10)
        {
            producedCount++;
            scrapCount -= 10f;
            UpdateUI();
        }
        //get reference to Fabricator Object
        //spawn robo part 1 and add to list
        //apply force to push onto conveyor from spawn

    }



    private void IncrementScrap()
    {
        scrapCount += 1 + scrapModifier * 1.0f;
    }


    public void addCash()
    {
        cashCount += 100.0f + cashModifier;
    }



    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + scrapCount;
        countText.text = "Robots Produced: " + producedCount;
        cashCountText.text = "Cash: " + cashCount;

    }


    private void ReloadData(PlayerData save)
    {
        producedCount = save.producedCount;
        scrapCount = save.scrapCount;
        cashCount = save.cashCount;
        loadTime = save.saveTime;
        int loadResources = (int)loadTime - (int)Time.time;
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



    private void OnApplicationQuit()
    {
        SaveData(save);
    }

}
