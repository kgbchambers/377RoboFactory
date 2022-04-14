using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public List<Upgrade> upgrades;

    private void Start()
    {
        upgrades = upgrades.OrderBy(up => up.cost).ToList();
        
    }


    public void UpgradeScrapCap(string op, float mod, float cost)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if(op == "x ")
            {
                GameManager.instance.scrapCap *= mod;
            }
            else if(op == "+ ")
            {
                GameManager.instance.scrapCap += mod;
            }
        }

        
    }

    public void UpgradeScrapRecharge(string op, float mod, float cost)
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
        }
    }

    public void UpgradeConveyorSpeed(string op, float mod, float cost)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                foreach (GameObject conveyor in GameManager.instance.Conveyors)
                {
                    conveyor.GetComponent<Conveyor>().speed *= mod;
                }
            }
            else if (op == "+ ")
            {
                foreach (GameObject conveyor in GameManager.instance.Conveyors)
                {
                    conveyor.GetComponent<Conveyor>().speed = conveyor.GetComponent<Conveyor>().speed + mod;
                }
            }
        }
    }


    public void UpgradeTruckCap(string op, float mod, float cost)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                GameManager.instance.truckCap *= mod;
            }
            else if (op == "+ ")
            {
                GameManager.instance.truckCap += mod;
            }
        }
    }

    public void UpgradeTruckSpeed(string op, float mod, float cost)
    {
        if (GameManager.instance.spendCash(cost))
        {
            if (op == "x ")
            {
                GameManager.instance.truckSpeed *= mod;
            }
            else if (op == "+ ")
            {
                GameManager.instance.truckSpeed += mod;
            }
        }
    }

    public void UpgradeFabricatorSpeed(string op, float mod, float cost)
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
        }
    }

    public void UpgradeRobotValue(string op, float mod, float cost)
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
        }
    }

}
