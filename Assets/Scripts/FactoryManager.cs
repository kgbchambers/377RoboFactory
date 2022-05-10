using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactoryManager : Singleton<FactoryManager>
{
    
    void Start()
    {
        IsDestroyedOnLoad = false;
        StartCoroutine(GetFactory());
        if(GameManager.instance.factoryTier < 1)
        {
            GameManager.instance.factoryTier = 1;
        }
    }


    public void ChangeFactory()
    {
        StartCoroutine(GetFactory());
    }



    private IEnumerator GetFactory()
    {
        yield return new WaitForSeconds(0.2f);
        int sceneToLoad = GameManager.instance.factoryTier;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneToLoad);
    }

}
