using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameObject SettingPanel;
  
    public void StartMusic()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Option()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
    }

    public void ReturnMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Return()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void SelectSong(SongData song)
    {
        SongManager.instance.selectedSong = song;
    }
}
