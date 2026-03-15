using System;
using UnityEngine;

public class VictoryButtons : MonoBehaviour
{
    private void Start()
    {
        // Liberamos el ratón para que sea visible y pueda interactuar con la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void VolverAlMenu()
    {
        // 1. Reseteamos los datos antes de salir si es necesario
        GameSceneManager.Instance.vidas = 3; 
        
        // 2. Cargamos el menú
        GameSceneManager.Instance.CargarEscena("MainMenu"); 
    }
}