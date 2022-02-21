using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    private PlayerInput touchControls;

    public Text countText;

    private float count;
    private float scrapCount;
    private float loadTime;



    private void Awake()
    {
        touchControls = new PlayerInput();
        touchControls.Enable();
    }


    private void Start()
    {
        count = 0;
        LoadGame();
        countText.text = "" + count;
    }


    public void ProductionButton()
    {
        count++;
        countText.text = "" + count;
    }




    private PlayerData CreateSaveGameObject()
    {
        PlayerData save = new PlayerData();

        save.count = count;
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

            count = save.count;
            scrapCount = save.scrapCount;
            loadTime = save.saveTime;
            int loadResources = (int)loadTime - (int)Time.time;
            count += loadResources;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No Game Save");
        }
    }



    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
