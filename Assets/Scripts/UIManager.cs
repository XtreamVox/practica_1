using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameObject hudContainer; 
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnEnable()
    {
        // Suscripción a eventos: La única forma de actualizar el HUD
        if (GameData.Instance != null)
        {
            GameData.Instance.OnVidasChanged += ActualizarHUD;
            GameData.Instance.OnPuntosChanged += ActualizarHUD;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        // Limpieza de suscripciones para evitar errores de memoria
        if (GameData.Instance != null)
        {
            GameData.Instance.OnVidasChanged -= ActualizarHUD;
            GameData.Instance.OnPuntosChanged -= ActualizarHUD;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool esEscenaDeJuego = scene.name == "MainScene";
    
        // En lugar de desactivar el script, desactivamos el contenedor visual
        if (hudContainer != null)
            hudContainer.SetActive(esEscenaDeJuego);
    
        if (esEscenaDeJuego) ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        if (GameData.Instance == null) return;

        // Actualizamos los textos usando las propiedades de GameData
        if (livesText != null) 
            livesText.text = "Rocks: " + GameData.Instance.Vidas;
        
        if (scoreText != null) 
            scoreText.text = "Score: " + GameData.Instance.Puntuacion;
            
        Debug.Log("HUD actualizado: Vidas " + GameData.Instance.Vidas);
    }
    // Dentro de UIManager.cs
    public void ActualizarScoreUI(int nuevoScore)
    {
        if (scoreText != null) scoreText.text = "Score: " + nuevoScore;
    }

    public void ActualizarVidasUI(int nuevasVidas)
    {
        if (livesText != null) livesText.text = "Rocks: " + nuevasVidas;
    }
}