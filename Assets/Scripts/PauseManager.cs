using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public GameObject objetoMenuPausa; 
    private bool estaPausado = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        objetoMenuPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado) Reanudar();
            else Pausar();
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        objetoMenuPausa.SetActive(true);
        Time.timeScale = 0f; // Congela el mundo físico
    }

    public void Reanudar()
    {
        estaPausado = false;
        objetoMenuPausa.SetActive(false);
        Time.timeScale = 1f; // Devuelve el tiempo a la normalidad
    }
    
}