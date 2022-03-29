using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuToggle : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject openMenuButton;
    
    public void ToggleMenuOn()
    {
        upgradeMenu.SetActive(true);
    }

    public void ToggleMenuOff()
    {
        upgradeMenu.SetActive(false);
    }
}
