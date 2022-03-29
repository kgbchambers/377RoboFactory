using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public Upgrade[] upgrades;

    private void Start()
    {
        upgrades = upgrades.OrderBy(p => p.tier).ToArray();
        foreach(Upgrade upgrade in upgrades)
        {
            Debug.Log(upgrade.upgradeName);
        }
    }
}
