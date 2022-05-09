using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour
{
    public GameObject pe;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Robot")
        {
            if(other.tag != "Selling")
            {
                other.tag = "Selling";
                StartCoroutine(Delay(other.gameObject));
            }
        }
    }
    IEnumerator Delay(GameObject Robo)
	{
        yield return new WaitForSeconds(5f);
        GameManager.instance.addCash();
        if (Robo != null)
		{
            Instantiate(pe, Robo.transform.position, Quaternion.identity);
            Destroy(Robo.gameObject);
		}
		
    }
}
