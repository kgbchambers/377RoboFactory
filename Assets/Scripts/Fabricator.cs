using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fabricator : MonoBehaviour
{

    private Transform spawnLocation;

    [Range(1,3)]
    public int processNumber;

    //change to read in list of all robots and manage ScriptableObjects (Robots)
    public Robot robotToProduce;


    public float spawnPower;


    private Rigidbody othersRB;

    public void Start()
    {
        spawnLocation = transform.Find("RobotSpawner");
    }

    public void buildRobot()
    {
        if (processNumber == 1)
        {
            GameObject part = Instantiate(robotToProduce.part1, spawnLocation);
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
                GameObject part = Instantiate(robotToProduce.part2, spawnLocation);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(transform.right * spawnPower, ForceMode.VelocityChange);
            }
            else if (processNumber == 3)
            {
                Destroy(other.gameObject);
                GameObject part = Instantiate(robotToProduce.part3, spawnLocation);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(transform.right * spawnPower, ForceMode.VelocityChange);
            }
            else if (processNumber == 4)
            {
                Destroy(other.gameObject);
                GameObject part = Instantiate(robotToProduce.part4, spawnLocation);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(transform.right * spawnPower, ForceMode.VelocityChange);
            }
        }
    }
}
