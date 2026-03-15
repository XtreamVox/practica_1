using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [Header("Configuración de Zona")]
    [SerializeField] private float resistenciaAgua = 2.5f; // Cuanto más alto, más se frena
    [SerializeField] private float fuerzaFlotacion = 10f;  // Fuerza hacia arriba

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            // Aplicar resistencia al entrar
            rb.linearDamping = resistenciaAgua;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            // Empujar hacia arriba suavemente mientras esté dentro
            rb.AddForce(Vector3.up * fuerzaFlotacion, ForceMode.Acceleration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            // Restaurar los valores normales al salir
            rb.linearDamping = 0f; 
        }
    }
}