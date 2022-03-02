using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    /*
    private PlayerData CreateSaveGameObject()
    {
        PlayerData save = new PlayerData();

        save.producedCount = producedCount;
        save.scrapCount = scrapCount;
        save.cashCount = cashCount;
        save.saveTime = Time.time;

        return save;
    }
    */


    public void SaveGame(PlayerData save)
    {
        //create a save instance with all the data for the current session save into it
        //PlayerData save = CreateSaveGameObject();

        //create a binary formatter and filestream passing a path for the save instance to be save to.
        //It will serialize the data (into bytes) and write to the disk and close the filestream.
        //There will then be a file name gamesave.save on the players computer - Note that we can use whatever we want
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);

        //reset the game state
        Debug.Log("Game Saved");
    }



    public PlayerData LoadGame()
    {
        //Check to see that the save file exists. If it does, clear the robots and score.
        //Otherwise, log to the console that there is no save game.

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            PlayerData save = (PlayerData)bf.Deserialize(file);
            file.Close();

            /*
            producedCount = save.producedCount;
            scrapCount = save.scrapCount;
            cashCount = save.cashCount;
            loadTime = save.saveTime;
            int loadResources = (int)loadTime - (int)Time.time;
            producedCount += loadResources;
            */
            Debug.Log("Game Loaded");
            return save;
        }
        else
        {
            Debug.Log("No Game Save");
            return new PlayerData();
        }
    }
}
