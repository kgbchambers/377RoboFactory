using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInventoryCreator : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPref;
    [SerializeField] private Transform ButtonContainer;
    private int upgradeCount;
   
    private void Start()
    {
        upgradeCount = 0;
        StartCoroutine(CreateInventory());
    }


    IEnumerator UpdateInventory()
    {
        yield return new WaitForEndOfFrame();
        foreach (Upgrade upgrade in UpgradeManager.instance.upgrades)
        {
            while (upgradeCount < 5)
            {
                GameObject instance = Instantiate(ButtonPref, ButtonContainer);
                instance.name = upgrade.name;
                UpgradeItem upgradeItem = instance.GetComponent<UpgradeItem>();
                upgradeItem.upgrade = upgrade;
                upgradeItem.NameText.text = "" + upgrade.upgradeName;
                upgradeItem.CostText.text = "" + upgrade.cost;
            }
        }
    }


    IEnumerator CreateInventory()
    {
        yield return new WaitForEndOfFrame();
        foreach (Upgrade upgrade in UpgradeManager.instance.upgrades)
        {
            while(upgradeCount < 5)
            {
                GameObject instance = Instantiate(ButtonPref, ButtonContainer);
                instance.name = upgrade.name;
                UpgradeItem upgradeItem = instance.GetComponent<UpgradeItem>();
                upgradeItem.upgrade = upgrade;
                upgradeItem.NameText.text = "" + upgrade.upgradeName;
                upgradeItem.CostText.text = "" + upgrade.cost;
            }
        }     
    }
}
