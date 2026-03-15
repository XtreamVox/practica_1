using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Instancia estática para el patrón Singleton
    public static GameSceneManager Instance { get; private set; }
    public int vidas = 3;

    public void PerderVida()
    {
        GameData.Instance.RestarVida();
        int vidasActuales = GameData.Instance.Vidas;

        if (UIManager.Instance != null) 
        {
            UIManager.Instance.ActualizarVidasUI(vidasActuales);
        }

        if (vidasActuales > 0)
        {
            ReiniciarEscenaActual();
        }
        else
        {
            GameData.Instance.ResetProgress();
            CargarEscena("GameOver");
        }
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
    
    public void ReiniciarEscenaActual()
    {
        Ball jugador = FindFirstObjectByType<Ball>();
        if (jugador != null) 
        {
            jugador.PrepararCambioEscena(); 
        }

        Time.timeScale = 1f;
        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}