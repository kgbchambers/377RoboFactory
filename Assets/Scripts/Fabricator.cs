using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fabricator : MonoBehaviour
{

    private Transform spawnLocation;

    [Range(1,4)]
    public int processNumber;
    public float speed;

    //change to read in list of all robots and manage ScriptableObjects (Robots)
    public Robot robotToProduce;
    public float spawnPower;

    private Rigidbody othersRB;
    private GameObject part;

    public void Start()
    {
        spawnLocation = transform.Find("RobotSpawner");
        spawnPower = 1.2f;
        speed = 3f;
    }

    public void buildRobot()
    {
        if (processNumber == 1)
        {
            GameObject part = Instantiate(robotToProduce.part1, spawnLocation);
            part.transform.localScale = new Vector3(12, 12, 12);
            othersRB = part.GetComponent<Rigidbody>();
            othersRB.AddForce(Vector3.right * spawnPower, ForceMode.VelocityChange);
        }
    }

  
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            Destroy(other.gameObject);
            StartCoroutine(FabricationDelay());
        }
    }


    IEnumerator FabricationDelay()
    {
        yield return new WaitForSeconds(speed);
        switch (processNumber)
        {
            case 2:
                part = Instantiate(robotToProduce.part2, spawnLocation);
                part.transform.localScale = new Vector3(12, 12, 12);

                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(Vector3.right * spawnPower, ForceMode.VelocityChange);
                break;
            case 3:

                part = Instantiate(robotToProduce.part3, spawnLocation);
                part.transform.localScale = new Vector3(12, 12, 12);

                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(Vector3.right * spawnPower, ForceMode.VelocityChange);
                break;
            case 4:
                part = Instantiate(robotToProduce.part4, spawnLocation);
                part.transform.localScale = new Vector3(12, 12, 12);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(Vector3.right * spawnPower, ForceMode.VelocityChange);
                break;
            default:
                break;
        }
    }
}
