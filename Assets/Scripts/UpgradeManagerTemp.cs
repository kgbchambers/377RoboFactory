using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeManagerTemp : Singleton<UpgradeManagerTemp>
{
    /*
    private float cashLevel = 1;
    private float scrapLevel = 1;
    private float fabLevel = 1;
    private float conveyorLevel = 1;
    
    private float cashCost = 1f;
    private float scrapCost = 1f;
    private float fabCost = 1f;
    private float conveyorCost = 1f;

    public TextMeshProUGUI cashLevelText;
    public TextMeshProUGUI scrapLevelText;
    public TextMeshProUGUI fabLevelText;
    public TextMeshProUGUI conveyorText;
    
    public TextMeshProUGUI cashCostText;
    public TextMeshProUGUI fabCostText;
    public TextMeshProUGUI conveyorCostText;


    private void Start()
    {
        cashCost = 100;
        scrapCost = 20;
        fabCost = 250;
        conveyorCost = 350;
        UpdateUI();
    }


    public void UpgradeScrap()
    {
        if (GameManager.instance.cashCount >= scrapCost)
        {
            scrapCost = scrapCost * (scrapLevel + 1)/2;
            scrapLevel++;
            GameManager.instance.UpgradeScrap(scrapLevel, scrapCost);
            UpdateUI();
            Debug.Log("Scrap Upgrade");
        }
    }
    
    public void UpgradeCash()
    {
        if(GameManager.instance.cashCount >= cashCost)
        {
            cashCost = cashCost * (cashLevel + 1)/2;
            cashLevel++;
            GameManager.instance.UpgradeCash(cashLevel, cashCost);
            UpdateUI();
        }
    }


    public void UpgradeFabricators()
    {
        if (GameManager.instance.cashCount >= fabCost)
        {
            fabCost = fabCost * (fabLevel + 1) / 2;
            fabLevel++;
            GameManager.instance.UpgradeFabs(fabLevel, fabCost);
            UpdateUI();
        }
    }


    public void UpgradeConveyors()
    {
        if (GameManager.instance.cashCount >= conveyorCost)
        {
            conveyorCost = conveyorCost * (conveyorLevel + 1)/2;
            conveyorLevel++;
            GameManager.instance.UpgradeConveyors(conveyorLevel, conveyorCost);
            UpdateUI();
        }
    }


    private void UpdateUI()
    {
        cashLevelText.text = "Robot lvl: " + cashLevel;
        cashCostText.text = "-" + cashCost;
        scrapLevelText.text = "Scrap lvl: " + scrapLevel;
        fabLevelText.text = "Fabricator lvl: " + fabLevel;
        fabCostText.text = "-" + fabCost;
        conveyorText.text = "Conveyor lvl: " + conveyorLevel;
        conveyorCostText.text = "-" + conveyorCost;
    }
    */
}
