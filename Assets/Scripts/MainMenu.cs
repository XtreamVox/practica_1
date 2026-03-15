using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f; // Asegurar que el tiempo fluye
    }

    public void IniciarJuego()
    {
        // Reseteamos vidas por si venimos de un GameOver
        GameSceneManager.Instance.vidas = 3;
        GameSceneManager.Instance.CargarEscena("MainScene");
    }

    public void Salir()
    {
        GameSceneManager.Instance.SalirDelJuego();
    }
}