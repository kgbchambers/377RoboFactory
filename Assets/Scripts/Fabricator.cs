using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : MonoBehaviour
{
    private Transform spawnLocation;

    [Range(1,3)]
    public int processNumber;

    //change to read in list of all robots and manage ScriptableObjects (Robots)
    public Robot robotToProduce;


    public float spawnPower;


    private Rigidbody othersRB;
    private Collider inputCollider;

    public void Start()
    {
        spawnLocation = transform.Find("RobotSpawner");
        if (processNumber == 1)
            inputCollider = GetComponent<BoxCollider>();

    }

    public void buildRobot()
    {
        if (processNumber == 1)
        {
            GameObject part = Instantiate(robotToProduce.legs, spawnLocation);
            othersRB = part.GetComponent<Rigidbody>();
            othersRB.AddForce(transform.right * spawnPower, ForceMode.VelocityChange);
        }
    }

  
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Robot")
        {
            if (processNumber == 2)
            {
                Destroy(other.gameObject);
                GameObject part = Instantiate(robotToProduce.chassisLegs, spawnLocation);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(transform.right * spawnPower, ForceMode.VelocityChange);
            }
            if (processNumber == 3)
            {
                Destroy(other.gameObject);
                GameObject part = Instantiate(robotToProduce.fullRobot, spawnLocation);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(transform.right * spawnPower, ForceMode.VelocityChange);
            }
        }
    }
}
