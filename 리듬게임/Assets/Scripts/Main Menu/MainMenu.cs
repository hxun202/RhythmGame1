using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Setting()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}
