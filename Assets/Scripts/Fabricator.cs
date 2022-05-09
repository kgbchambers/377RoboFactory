using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fabricator : MonoBehaviour
{

    public int _robotCounter;

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
        spawnPower = 2f;
        speed = 2f;
        StartCoroutine(FabricatorQueue());
    }


    public void buildRobot()
    {
        if (processNumber == 1)
        {
            lightbulb.GetComponent<MeshRenderer>().material = lighton;

            int rand = Random.Range(0, 3);
            if(rand == 0)
			{
                GameObject part = Instantiate(robotToProduce.small1, spawnLocation.position, Quaternion.identity);
                othersRB = part.GetComponent<Rigidbody>();
            }
            else if (rand == 1)
			{
                GameObject part = Instantiate(robotToProduce.small2, spawnLocation.position, Quaternion.identity);
                othersRB = part.GetComponent<Rigidbody>();
            }
            else if (rand == 2)
			{
                GameObject part = Instantiate(robotToProduce.small3, spawnLocation.position, Quaternion.identity);
                othersRB = part.GetComponent<Rigidbody>();
            }
            //part.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            
            othersRB.AddForce(spawnLocation.right * spawnPower, ForceMode.VelocityChange);

            //animate the fabricator after spawning part
            animator.SetTrigger("trigger");
            StartCoroutine(LightDelay());

        }
    }

    //creating coroutine for delay on light turning off
    IEnumerator LightDelay()
	{
        yield return new WaitForSeconds(0.1f);
        lightbulb.GetComponent<MeshRenderer>().material = lightoff;
    }
  
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            _robotCounter++;
            Destroy(other.gameObject);
            //change lightbulb material
            lightbulb.GetComponent<MeshRenderer>().material = lighton;

            //StartCoroutine(FabricationDelay());
        }
    }



    IEnumerator FabricatorQueue()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if(_robotCounter > 0)
            {
                yield return new WaitForSeconds(speed);
                switch (processNumber)
                {
                    case 2:

                        //change lightbulb material
                        lightbulb.GetComponent<MeshRenderer>().material = lighton;

                        int rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            GameObject part = Instantiate(robotToProduce.med1, spawnLocation.position, Quaternion.identity);
                            othersRB = part.GetComponent<Rigidbody>();
                        }
                        else if (rand == 1)
                        {
                            GameObject part = Instantiate(robotToProduce.med2, spawnLocation.position, Quaternion.identity);
                            othersRB = part.GetComponent<Rigidbody>();
                        }
                        else if (rand == 2)
                        {
                            GameObject part = Instantiate(robotToProduce.med3, spawnLocation.position, Quaternion.identity);
                            othersRB = part.GetComponent<Rigidbody>();
                        }

                        othersRB.AddForce(spawnLocation.right * (spawnPower + 10), ForceMode.VelocityChange);

                        //animate the fabricator after spawning part
                        animator.SetTrigger("trigger");
                        lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                        _robotCounter--;
                        break;

                    case 3:
                        //change lightbulb material
                        lightbulb.GetComponent<MeshRenderer>().material = lighton;

                        part = Instantiate(robotToProduce.full, spawnLocation.position, Quaternion.identity);

                        othersRB = part.GetComponent<Rigidbody>();
                        othersRB.AddForce(spawnLocation.right * (spawnPower + 20), ForceMode.VelocityChange);

                        //animate the fabricator after spawning part
                        animator.SetTrigger("trigger");
                        lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                        _robotCounter--;
                        break;

                    case 4:
                        //change lightbulb material
                        lightbulb.GetComponent<MeshRenderer>().material = lighton;

                        part = Instantiate(robotToProduce.box, spawnLocation.position, Quaternion.identity);

                        othersRB = part.GetComponent<Rigidbody>();
                        othersRB.AddForce(spawnLocation.right * (spawnPower + 10), ForceMode.VelocityChange);

                        //animate the fabricator after spawning part
                        animator.SetTrigger("trigger");
                        lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                        _robotCounter--;
                        break;
                    default:
                        break;
                }

            }
        }
    }



    /*

    IEnumerator FabricationDelay()
    {
        yield return new WaitForSeconds(speed);
        switch (processNumber)
        {
            case 2:

                lightbulb.GetComponent<MeshRenderer>().material = lightoff;

                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    GameObject part = Instantiate(robotToProduce.med1, spawnLocation.position, Quaternion.identity);
                    othersRB = part.GetComponent<Rigidbody>();
                }
                else if (rand == 1)
                {
                    GameObject part = Instantiate(robotToProduce.med2, spawnLocation.position, Quaternion.identity);
                    othersRB = part.GetComponent<Rigidbody>();
                }
                else if (rand == 2)
                {
                    GameObject part = Instantiate(robotToProduce.med3, spawnLocation.position, Quaternion.identity);
                    othersRB = part.GetComponent<Rigidbody>();
                }

                othersRB.AddForce(spawnLocation.right * (spawnPower+10), ForceMode.VelocityChange);

                //animate the fabricator after spawning part
                animator.SetTrigger("trigger");
                break;
            case 3:

                lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                part = Instantiate(robotToProduce.full, spawnLocation.position, Quaternion.identity);

                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(spawnLocation.right * (spawnPower+20), ForceMode.VelocityChange);

                //animate the fabricator after spawning part
                animator.SetTrigger("trigger");
                break;
            case 4:

                lightbulb.GetComponent<MeshRenderer>().material = lightoff;
                part = Instantiate(robotToProduce.box, spawnLocation.position, Quaternion.identity);

                othersRB = part.GetComponent<Rigidbody>();
                othersRB.AddForce(spawnLocation.right * (spawnPower+10), ForceMode.VelocityChange);

                //animate the fabricator after spawning part
                animator.SetTrigger("trigger");
                break;
            default:
                break;
        }
    }

    */

}
