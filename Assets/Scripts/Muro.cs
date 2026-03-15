using System;
using UnityEngine;

public class Muro : MonoBehaviour
{
    private float damage = 0;
    [SerializeField]private float salud = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (salud <= 0)
        {
            Destroy(this);
        }
    }
    public void UpdateHealth(float fuerzaImpacto)
    {
        // Opcional: puedes añadir un multiplicador si el daño es muy bajo
        damage = fuerzaImpacto; 
    
        salud -= damage;
        Debug.Log("Daño recibido: " + damage + " | Salud restante: " + salud);

        if (salud <= 0) 
        {
            // Importante: Destroy(this) solo borra el SCRIPT. 
            // Si quieres borrar el muro entero, usa Destroy(gameObject).
            Destroy(gameObject); 
        }
        else
        {
            GameSceneManager.Instance.PerderVida();
        }
    }
    
}
