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
        }
    }
    IEnumerator Delay(Collider Robo)
	{
        yield return new WaitForSeconds(5f);
        Destroy(Robo.gameObject);
        GameManager.instance.addCash();
    }
}
