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

    //getting related animation things
    public Animator animator;
    public Material lightoff;
    public Material lighton;
    public GameObject lightbulb;

    private Rigidbody othersRB;
    private GameObject part;

    public void Start()
    {
        spawnLocation = transform.Find("RobotSpawner");
        spawnPower = 1.3f;
        speed = 3f;
    }

    public void buildRobot()
    {
        if (processNumber == 1)
        {
            GameObject part = Instantiate(robotToProduce.small1, spawnLocation);
            part.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
            othersRB = part.GetComponent<Rigidbody>();
            othersRB.AddForce(spawnLocation.right * spawnPower, ForceMode.VelocityChange);

            //animate the fabricator after spawning part
            animator.SetTrigger("trigger");
        }
    }

  
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            Destroy(other.gameObject);

            //change lightbulb material
            lightbulb.GetComponent<MeshRenderer> ().material = lighton;

            StartCoroutine(FabricationDelay());
        }
    }


    IEnumerator FabricationDelay()
    {
        yield return new WaitForSeconds(speed);
        switch (processNumber)
        {
            case 2:

                lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                part = Instantiate(robotToProduce.med1, spawnLocation);
                part.transform.localScale = new Vector3(1f, 1f, 1f);

                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(spawnLocation.right * spawnPower, ForceMode.VelocityChange);

                //animate the fabricator after spawning part
                animator.SetTrigger("trigger");
                break;
            case 3:

                lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                part = Instantiate(robotToProduce.med1, spawnLocation);
                part.transform.localScale = new Vector3(1f, 1f, 1f);

                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(spawnLocation.right * spawnPower, ForceMode.VelocityChange);

                //animate the fabricator after spawning part
                animator.SetTrigger("trigger");
                break;
            case 4:

                lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                part = Instantiate(robotToProduce.final, spawnLocation);
                part.transform.localScale = new Vector3(1f, 1f, 1f);
                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(spawnLocation.right * spawnPower, ForceMode.VelocityChange);

                //animate the fabricator after spawning part
                animator.SetTrigger("trigger");
                break;
            default:
                break;
        }
    }
}
