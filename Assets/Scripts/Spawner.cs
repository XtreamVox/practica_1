using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]private GameObject bouncyBallPrefab;

    [SerializeField] private float force = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Quaternion.identity: es (0,0,0) elemento neutro
        // Quaternion.Euler (xº, yº, zº)
        // transform.rotation: rotación del spawner
        Instantiate(bouncyBallPrefab, transform.position, Quaternion.identity);
        //StartCoroutine(Semaforo());
        StartCoroutine(SpawnBalls());
    }
    
    // Corrutina: Como los hilos (programación concurrente)
    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            GameObject copy = Instantiate(bouncyBallPrefab, transform.position, Quaternion.identity);
            copy.GetComponent<Rigidbody>().AddForce(-this.transform.right * force, ForceMode.Impulse);
            yield return new WaitForSeconds(2.5f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
