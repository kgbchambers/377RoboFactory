using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed;
    public bool isRotated;
    public bool isNegativeRotated;
    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        if(isRotated)
            rBody.position += Vector3.forward * speed * Time.fixedDeltaTime;
        else if(isNegativeRotated)
            rBody.position += Vector3.back * speed * Time.fixedDeltaTime;
        else
            rBody.position += Vector3.left * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }
}
