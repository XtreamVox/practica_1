using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField]public GameObject objetoMenuPausa; 
    private bool estaPausado = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        Cursor.visible = estaPausado;
        Cursor.lockState = estaPausado ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Start()
    {
        objetoMenuPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name.Equals("MainScene"))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        estaPausado = !estaPausado;

        Time.timeScale = estaPausado ? 0f : 1f; 
        objetoMenuPausa.SetActive(estaPausado);

        UpdateCursorState();
    }
    
    private void UpdateCursorState()
    {
        Cursor.visible = estaPausado;
        Cursor.lockState = estaPausado ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void Resume()
    {
        TogglePause();
    }
    
    public void Restart()
    {
        TogglePause();
        GameSceneManager.Instance.ReiniciarEscenaActual();
        GameSceneManager.Instance.vidas = 3;
        FindFirstObjectByType<Ball>().Score = 0.0f;
        UIManager.Instance.ScoreText.SetText("Score: " + 0);

    }
    public void BackToMenu()
    {
        Ball bola = FindFirstObjectByType<Ball>();
        if (bola != null)
        {
            bola.PrepararCambioEscena(); // Aquí le decimos que "no ha muerto, solo nos vamos"
        }
        TogglePause();
        GameSceneManager.Instance.CargarEscena("MainMenu");
        
    }
}