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

    private float producedCount;
    private float scrapCount;
    private float loadTime;
    private float scrapAddBonus;

    //private List<Parts> roboParts;



    private void Start()
    {
        touchControls = new PlayerInput();
        touchControls.Enable();

        producedCount = 0;
        scrapAddBonus = 0;
        LoadGame();
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
        if(scrapCount >= 10)
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
        scrapCount += scrapAddBonus + 1f;
    }



    private void UpdateUI()
    {
        scrapCountText.text = "Scrap: " + scrapCount;
        countText.text = "Robots Produced: " + producedCount;
    }



    private PlayerData CreateSaveGameObject()
    {
        PlayerData save = new PlayerData();

        save.producedCount = producedCount;
        save.scrapCount = scrapCount;
        save.saveTime = Time.time;
 
        return save;
    }



    public void SaveGame()
    {
        //create a save instance with all the data for the current session save into it
        PlayerData save = CreateSaveGameObject();

        //create a binary formatter and filestream passing a path for the save instance to be save to.
        //It will serialize the data (into bytes) and write to the disk and close the filestream.
        //There will then be a file name gamesave.save on the players computer - Note that we can use whatever we want
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);

        //reset the game state
        Debug.Log("Game Saved");
    }



    public void LoadGame()
    {
        //Check to see that the save file exists. If it does, clear the robots and score.
        //Otherwise, log to the console that there is no save game.

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            PlayerData save = (PlayerData)bf.Deserialize(file);
            file.Close();

            producedCount = save.producedCount;
            scrapCount = save.scrapCount;
            loadTime = save.saveTime;
            int loadResources = (int)loadTime - (int)Time.time;
            producedCount += loadResources;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No Game Save");
        }
        UpdateUI();
    }



    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
