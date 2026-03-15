using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Instancia estática para el patrón Singleton
    public static GameSceneManager Instance { get; private set; }
    public int vidas = 3;

    public void PerderVida()
    {
        vidas--;
        Debug.Log("Vidas restantes: " + vidas);
        if (UIManager.Instance != null) 
        {
            UIManager.Instance.ActualizarVidas(vidas);
        }
        if (vidas > 0)
        {
            ReiniciarEscenaActual();
        }
        else
        {
            // Opcional: Cargar escena de Game Over
            CargarEscena("GameOver"); 
            vidas = 3; // Resetear para la próxima partida
        }
        
    }
    
    private void Awake()
    {
        // Lógica del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        // Hace que este objeto persista entre escenas
        DontDestroyOnLoad(this.gameObject);
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
    
    public void ReiniciarEscenaActual()
    {
        // Buscamos a la bola y le decimos que no reste vida al destruirse
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