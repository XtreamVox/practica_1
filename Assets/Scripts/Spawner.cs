using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]private GameObject bouncyBallPrefab;
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
            copy.GetComponent<Rigidbody>().AddForce(Vector3.forward * Random.Range(0f, 20f), ForceMode.Impulse);
            Destroy(copy.gameObject, 2f);
            yield return new WaitForSeconds(1f);
        }
    }
  
    private IEnumerator Semaforo()
    {
        Debug.Log("Verde");
        yield return new WaitForSeconds(2f);        
        Debug.Log("Amarillo");
        yield return new WaitForSeconds(1f);        
        Debug.Log("Rojo");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
