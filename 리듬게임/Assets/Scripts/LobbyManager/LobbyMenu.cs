using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour
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

    public void StartSong()
    {
        SongManager.instance.StopPreview();

        SceneManager.LoadScene("InGame");
    }
    
    public void SelectSong(MusicData song)
    {
        SongManager.instance.selectedSong = song;
        Debug.Log("SelectSong : " + song.songName);
    }
}
