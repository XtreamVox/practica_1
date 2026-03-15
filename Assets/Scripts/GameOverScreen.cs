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
        GameSceneManager.Instance.vidas = 3;
        GameSceneManager.Instance.CargarEscena("MainScene");
    }

    public void IrAlMenu()
    {
        GameSceneManager.Instance.CargarEscena("MainMenu");
    }
}