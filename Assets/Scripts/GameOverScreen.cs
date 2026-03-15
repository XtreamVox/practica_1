using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reintentar()
    {
        if (GameData.Instance != null)
        {
            GameData.Instance.ResetProgress(); 
        }
        
        GameSceneManager.Instance.CargarEscena("MainScene");
    }

    public void BackToMenu()
    {
        Ball bola = FindFirstObjectByType<Ball>();
        if (bola != null)
        {
            bola.PrepararCambioEscena();
        }
        if (GameData.Instance != null)
        {
            GameData.Instance.ResetProgress();
        }

        GameSceneManager.Instance.CargarEscena("MainMenu");

    }
}