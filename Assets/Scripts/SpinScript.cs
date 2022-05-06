using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public GameObject RobotSpin;
    

    void FixedUpdate()
    {
        RobotSpin.transform.Rotate(0, 1f, 0);
    }
}
