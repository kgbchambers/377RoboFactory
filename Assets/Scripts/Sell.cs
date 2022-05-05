using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Robot")
        {
            StartCoroutine(Delay(other));
            GameManager.instance.addCash();
        }
    }
    IEnumerator Delay(GameObject Robo)
	{
        yield return new WaitForSeconds(5f);
        Destroy(Robo.gameObject);
    }
}
