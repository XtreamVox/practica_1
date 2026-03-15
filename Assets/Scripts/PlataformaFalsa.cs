using System;
using UnityEngine;

public class PlataformaFalsa : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    // He colisionado
    private void OnCollisionEnter(Collision other)
    {
        // El rigidBody no se puede activar/desactivar, pero si que puede cambiar de estado
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
