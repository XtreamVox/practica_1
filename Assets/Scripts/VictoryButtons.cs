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
        if (GameData.Instance != null)
        {
            GameData.Instance.ResetProgress();
        }

        GameSceneManager.Instance.CargarEscena("MainMenu");

    }
}