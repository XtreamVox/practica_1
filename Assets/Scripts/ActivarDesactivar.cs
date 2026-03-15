using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ActivarDesactivar : MonoBehaviour
{
    private MeshRenderer mesh;
    private BoxCollider box;
    private bool on = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
        // SetActive activa y desactiva un gameObject junto con su script
        StartCoroutine(EncenderApagar());
    }
    
    private IEnumerator EncenderApagar()
    {
        while (true)
        {
            on = !on;
            mesh.enabled = on;
            box.enabled = !on;

            yield return new WaitForSeconds(1.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
