using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongList : MonoBehaviour
{

    public AudioSource audioSource;

    public SongData[] songs;

    public Image albumImage;

    public TMP_Text songNameText;
    public TMP_Text composerText;

    public GameObject songDetailsPanel;

    public void ShowSong(SongData song)
    {
        songDetailsPanel.SetActive(true);

        audioSource.Stop();

        audioSource.clip = song.clip;

        audioSource.Play(); 

        albumImage.sprite = song.image;
        songNameText.text = song.songName;
        composerText.text = song.composer;
    }
}
