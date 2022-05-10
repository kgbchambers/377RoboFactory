using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeInventoryCreator : Singleton<UpgradeInventoryCreator>
{
    [SerializeField] private GameObject ButtonPref;
    [SerializeField] private Transform ButtonContainer;


    public List<GameObject> upgradeButtons;
    public int upgradeCount;
    private string op;
    private string modName;

    private int conveyorTier;
    private int robotTier;
    private int fabricatorTier;
    private int scrapRechargeTier;
    private int scrapCapTier;

    private void Start()
    {
        IsDestroyedOnLoad = true;
        upgradeCount = 0;
        StartCoroutine(CreateInventory());

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

        
    }



    /*
    IEnumerator UpdateInventory(string upgradeToAdd)
    {
        yield return new WaitForEndOfFrame();
        switch (upgradeToAdd)
        {
            case "conveyor":
                GameObject instance = Instantiate(ButtonPref, ButtonContainer);
                instance.name =.name;
                break;
            case "fabricator":

                break;
            case "scrapCap":

                break;
            case "scrapRecharge":

                break;
            case "robot":

                break;
        }
        
    }
    */

    IEnumerator CreateInventory()
    {
        yield return new WaitForEndOfFrame();
        foreach (Upgrade upgrade in UpgradeManager.instance.upgrades)
        {
                GameObject instance = Instantiate(ButtonPref, ButtonContainer);
                instance.name = upgrade.name;
                UpgradeItem upgradeItem = instance.GetComponent<UpgradeItem>();
                upgradeItem.upgrade = upgrade;
                upgradeItem.NameText.text = "" + upgrade.upgradeName;
                upgradeItem.CostText.text = "" + upgrade.cost;

                if (upgradeItem.upgrade.makeModifierMultiplicative)
                    op = "x ";
                else
                    op = "+ ";

                if (upgradeItem.upgrade.isModifyingScrapCapacity)
                {
                    modName = " scrap capacity";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeScrapCap(op, upgrade.modifier, upgrade.cost, instance); });
                }
                else if (upgradeItem.upgrade.isModifyingScrapRecharge)
                {
                    modName = " scrap recharge";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeScrapRecharge(op, upgrade.modifier, upgrade.cost, instance); });
                }
                else if (upgradeItem.upgrade.isModifyingConveyorSpeed)
                {
                    modName = " conveyor speed";
                    upgradeItem.upgrade.modifier = 1f;
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeConveyorSpeed(op, upgrade.modifier, upgrade.cost, instance); });

                }
                else if (upgradeItem.upgrade.isModifyingFabricatorSpeed)
                {
                    modName = " fabricator speed";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeFabricatorSpeed(op, upgrade.modifier, upgrade.cost, instance); });
                }
                else if (upgradeItem.upgrade.isModifyingRobotValue)
                {
                    modName = " Robots value";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeRobotValue(op, upgrade.modifier, upgrade.cost, instance); });
                }

                upgradeItem.ModifierText.text = op + upgradeItem.upgrade.modifier + modName;
                upgradeCount++;
                UpgradeManager.instance.upgradeButtons.Add(instance);
        }
       
    }





    /*
    IEnumerator CreateInventory()
    {
        yield return new WaitForEndOfFrame();
        foreach (Upgrade upgrade in UpgradeManager.instance.upgrades)
        {
                GameObject instance = Instantiate(ButtonPref, ButtonContainer);
                instance.name = upgrade.name;
                UpgradeItem upgradeItem = instance.GetComponent<UpgradeItem>();
                upgradeItem.upgrade = upgrade;
                upgradeItem.NameText.text = "" + upgrade.upgradeName;
                upgradeItem.CostText.text = "" + upgrade.cost;

                if (upgradeItem.upgrade.makeModifierMultiplicative)
                    op = "x ";
                else
                    op = "+ ";

                if (upgradeItem.upgrade.isModifyingScrapCapacity)
                {
                    modName = " scrap capacity";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeScrapCap(op, upgrade.modifier, upgrade.cost); });
                }
                else if (upgradeItem.upgrade.isModifyingScrapRecharge)
                {
                    modName = " scrap recharge";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeScrapRecharge(op, upgrade.modifier, upgrade.cost); });
                }
                else if (upgradeItem.upgrade.isModifyingConveyorSpeed)
                {
                    modName = " conveyor speed";
                    upgradeItem.upgrade.modifier = 1f;
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeConveyorSpeed(op, upgrade.modifier, upgrade.cost); });

                }
                else if (upgradeItem.upgrade.isModifyingTruckCapacity)
                {
                    modName = " truck capacity";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeTruckCap(op, upgrade.modifier, upgrade.cost); });

                }

                else if (upgradeItem.upgrade.isModifyingTruckSpeed)
                {
                    modName = " truck speed";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeTruckSpeed(op, upgrade.modifier, upgrade.cost); });

                }

                else if (upgradeItem.upgrade.isModifyingFabricatorSpeed)
                {
                    modName = " fabricator speed";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeRobotValue(op, upgrade.modifier, upgrade.cost); });
                }
                else if (upgradeItem.upgrade.isModifyingRobotValue)
                {
                    modName = " Robots value";
                    instance.GetComponent<Button>().onClick.AddListener(delegate () { UpgradeManager.instance.UpgradeRobotValue(op, upgrade.modifier, upgrade.cost); });
                }

                upgradeItem.ModifierText.text = op + upgradeItem.upgrade.modifier + modName;
                upgradeCount++;
        }   
    }
    */



}
