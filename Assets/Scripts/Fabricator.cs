using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : MonoBehaviour
{
    private Transform spawnLocation;
    public bool isSpawn;
    public Robot robotToProduce;
    public float spawnPower;

    private Rigidbody rb;
    private Collider inputCollider;

    public void Start()
    {
        spawnLocation = transform.Find("RobotSpawner");
        if (!isSpawn)
            inputCollider = GetComponent<BoxCollider>();

    }

    public void buildRobot()
    {
        if (isSpawn)
        {
            GameObject part = Instantiate(robotToProduce.legs, spawnLocation);
            rb = part.GetComponent<Rigidbody>();
            rb.AddForce(transform.right * spawnPower);
        }
    }

  
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Robot")
        {
            Destroy(other.gameObject);
            GameObject part = Instantiate(robotToProduce.chassisLegs, spawnLocation);
            rb = part.GetComponent<Rigidbody>();
            rb.AddForce(transform.right * spawnPower);
        }
    }

}
