using UnityEngine;

public class VictoryButtons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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