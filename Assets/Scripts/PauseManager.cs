using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] public GameObject objetoMenuPausa;
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
        // 1. Quitar la pausa
        TogglePause();

        // 2. Resetear datos (esto dispara los eventos automáticamente)
        GameData.Instance.ResetProgress();

        // 3. Reiniciar la escena
        GameSceneManager.Instance.ReiniciarEscenaActual();
    }

    public void BackToMenu()
    {
        Ball bola = FindFirstObjectByType<Ball>();
        if (bola != null)
        {
            bola.PrepararCambioEscena();
        }
        TogglePause();
        if (GameData.Instance != null)
        {
            GameData.Instance.ResetProgress();
        }

        GameSceneManager.Instance.CargarEscena("MainMenu");

    }
}