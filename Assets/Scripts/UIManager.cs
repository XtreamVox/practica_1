using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public TMP_Text ScoreText { get; private set; }
    [field: SerializeField] public TMP_Text LivesText  { get; private set; }

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            // Nos suscribimos al evento de carga de escena
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // Muy importante: desuscribirse para evitar fugas de memoria
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

// Este método se ejecuta AUTOMÁTICAMENTE cada vez que cambia la escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscamos los nuevos textos en la escena recién cargada
        // Puedes usar Tags o buscarlos por tipo si solo hay uno
        ScoreText = GameObject.Find("ScoreText")?.GetComponent<TMP_Text>();
        LivesText = GameObject.Find("LivesText")?.GetComponent<TMP_Text>();
    
        // Sincronizamos los valores actuales nada más cargar
        if(GameSceneManager.Instance != null)
        {
            ActualizarVidas(GameSceneManager.Instance.vidas);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Al empezar el nivel, pedimos las vidas actuales al Manager
        ActualizarVidas(GameSceneManager.Instance.vidas);
    }

    public void ActualizarVidas(int cantidad)
    {
        LivesText.SetText("Rocks: " + cantidad);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SceneManager.LoadScene("Ejemplo");
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("SampleScene");
    }
}
