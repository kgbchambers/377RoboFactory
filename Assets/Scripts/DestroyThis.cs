using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    private Vector3 yOffset = new Vector3(0f, 3f, 0f);
    public GameObject pe;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
            StartCoroutine(WaitForDestroy(other.gameObject));
    }


    IEnumerator WaitForDestroy(GameObject robo)
    {
        yield return new WaitForSeconds(20f);
		if (robo != null)
		{
            if(pe != null)
                Instantiate(pe, robo.transform.position + yOffset , Quaternion.identity);
            Destroy(robo);
		}
	}
}
