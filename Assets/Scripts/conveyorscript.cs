using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorscript : MonoBehaviour
{
    public float speed;
    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        rBody.position += Vector3.back * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }
}
