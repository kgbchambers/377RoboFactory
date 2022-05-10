using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public List<GameObject> upgradeButtons;
    public List<Upgrade> upgrades;
    public List<float> tempSpeeds;

    private int conveyorTier;
    private int robotTier;
    private int fabricatorTier;
    private int scrapRechargeTier;
    private int scrapCapTier;




    private void Start()
    {
        if (!PlayerPrefs.HasKey("SaveCheck"))
        {
            PlayerPrefs.SetInt("SaveCheck", 1);
            PlayerPrefs.SetInt("scrapRechargeTier", scrapRechargeTier);
            PlayerPrefs.SetInt("scrapCapTier", scrapCapTier);
            PlayerPrefs.SetInt("conveyorTier", conveyorTier);
            PlayerPrefs.SetInt("fabricatorTier", fabricatorTier);
            PlayerPrefs.SetInt("robotTier", robotTier);
            PlayerPrefs.Save();
        }
        else
        {
            scrapRechargeTier = PlayerPrefs.GetInt("scrapRechargeTier");
            scrapCapTier = PlayerPrefs.GetInt("scrapCapTier");
            conveyorTier = PlayerPrefs.GetInt("conveyorTier");
            fabricatorTier = PlayerPrefs.GetInt("fabricatorTier");
            robotTier = PlayerPrefs.GetInt("robotTier");
        }
        tempSpeeds = new List<float>();

        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.isModifyingConveyorSpeed)
            {
                tempSpeeds.Add(upgrade.tierMod1);
                tempSpeeds.Add(upgrade.tierMod2);
                tempSpeeds.Add(upgrade.tierMod3);
                tempSpeeds.Add(upgrade.tierMod4);
                tempSpeeds.Add(upgrade.tierMod5);
                tempSpeeds.Add(upgrade.tierMod6);
                tempSpeeds.Add(upgrade.tierMod7);
                tempSpeeds.Add(upgrade.tierMod8);
                tempSpeeds.Add(upgrade.tierMod9);
            }
        }

        //GameManager.instance.speeds = tempSpeeds;

        StartCoroutine(SetManagerSpeeds());
        upgrades = upgrades.OrderBy(up => up.cost).ToList();

    }

    
    public IEnumerator SetManagerSpeeds()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.speeds = tempSpeeds;
        GameManager.instance.curConveyorTier = GameManager.instance.conveyorTier;


    }



    public void RecalculateCost()
    {

    }



    public void UpgradeScrapCap(string op, float mod, float cost, GameObject buttonToDoom)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                GameManager.instance.scrapCap *= mod;
            }
            else if (op == "+ ")
            {
                GameManager.instance.scrapCap += mod;
            }
            //upgradeButtons.Remove(buttonToDoom);
            //Destroy(buttonToDoom);
            //UpgradeInventoryCreator.instance.upgradeCount--;
            buttonToDoom.GetComponent<UpgradeItem>().CostText.text = "" + cost * 2;
            buttonToDoom.GetComponent<Button>().onClick.AddListener(delegate () { instance.UpgradeScrapCap(op, mod * 2, cost * 2, buttonToDoom); });

        }


    }



    public void UpgradeScrapRecharge(string op, float mod, float cost, GameObject buttonToDoom)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                GameManager.instance.scrapRecharge *= mod;
            }
            else if (op == "+ ")
            {
                GameManager.instance.scrapRecharge += mod;
            }
            buttonToDoom.GetComponent<UpgradeItem>().CostText.text = "" + cost * 2;
            buttonToDoom.GetComponent<Button>().onClick.AddListener(delegate () { instance.UpgradeScrapRecharge(op, mod * 2, cost * 2, buttonToDoom); });

        }
    }




    public void UpgradeConveyorSpeed(string op, float mod, float cost, GameObject buttonToDoom)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                GameManager.instance.UpgradeConveyor();
            }
            else if (op == "+ ")
            {
                GameManager.instance.UpgradeConveyor();
               
            }
            buttonToDoom.GetComponent<UpgradeItem>().CostText.text = "" + cost * 2;
            buttonToDoom.GetComponent<Button>().onClick.AddListener(delegate () { instance.UpgradeConveyorSpeed(op, mod * 2, cost * 2, buttonToDoom); });

        }
    }



    public void UpgradeFabricatorSpeed(string op, float mod, float cost, GameObject buttonToDoom)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                foreach (GameObject fabricator in GameManager.instance.Fabricators)
                {
                    fabricator.GetComponent<Fabricator>().speed *= mod;
                }
            }
            else if (op == "+ ")
            {
                foreach (GameObject fabricator in GameManager.instance.Fabricators)
                {
                    fabricator.GetComponent<Fabricator>().speed += mod;
                }
            }
            buttonToDoom.GetComponent<UpgradeItem>().CostText.text = "" + cost * 2;
            buttonToDoom.GetComponent<Button>().onClick.AddListener(delegate () { instance.UpgradeFabricatorSpeed(op, mod * 2, cost * 2, buttonToDoom); });

        }
    }

    public void UpgradeRobotValue(string op, float mod, float cost, GameObject buttonToDoom)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                GameManager.instance.robotValue *= mod;
            }
            else if (op == "+ ")
            {
                GameManager.instance.robotValue += mod;
            }
            buttonToDoom.GetComponent<UpgradeItem>().CostText.text = "" + cost * 2;
            buttonToDoom.GetComponent<Button>().onClick.AddListener(delegate () { instance.UpgradeRobotValue(op, mod * 2, cost * 2, buttonToDoom); });

        }
    }

}
