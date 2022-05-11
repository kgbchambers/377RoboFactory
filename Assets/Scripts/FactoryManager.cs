using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactoryManager : Singleton<FactoryManager>
{
    int factoryTier;
    
    void Start()
    {
        IsDestroyedOnLoad = true;
        factoryTier = PlayerPrefs.GetInt("factoryTier");
        if(factoryTier < 1 || factoryTier > 5)
        {
            factoryTier = 1;
            PlayerPrefs.SetInt("factoryTier", factoryTier);
            PlayerPrefs.Save();
        }
        ChangeFactory();
    }


    public void ChangeFactory()
    {
        StartCoroutine(GetFactory());
    }



    private IEnumerator GetFactory()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(factoryTier);
    }

}
