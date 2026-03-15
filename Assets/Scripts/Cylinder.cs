using System;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Es lo mismo que impulse pero sin considerar masa
        rb.AddTorque(Vector3.up, ForceMode.VelocityChange);

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0,360,0)* Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        //  rb.AddTorque(Vector3.up, ForceMode.Acceleration);

    }
}
