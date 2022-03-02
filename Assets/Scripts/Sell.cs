using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Robot")
        {
            Destroy(other.gameObject);
            GameManager.instance.addCash();
        }
    }
}
