using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactoryManager : Singleton<FactoryManager>
{
    
    void Start()
    {
        StartCoroutine(GetFactory());
    }


    public void ChangeFactory()
    {
        StartCoroutine(GetFactory());
    }



    private IEnumerator GetFactory()
    {
        int sceneToLoad = GameManager.instance.factoryTier;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneToLoad);
    }

}
