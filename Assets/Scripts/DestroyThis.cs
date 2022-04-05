using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
            StartCoroutine(WaitForDestroy(other.gameObject));
    }


    IEnumerator WaitForDestroy(GameObject robo)
    {
        yield return new WaitForSeconds(20f);
        Destroy(robo);
    }
}
