using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SongCover : MonoBehaviour
{
    public GameObject coverPanel;

    public Image coverImage;

    public TMP_Text songName;

    public TMP_Text level;

    public AudioSource music;

    public AudioClip coverClip;

    public Difficulty difficulty;

    IEnumerator Start()
    {
        SongData song = SongManager.instance.selectedSong;

        coverImage.sprite = song.image;

        songName.text = song.songName;

        switch (difficulty)
        {
            case Difficulty.Easy:
                level.text = "Easy " + song.easyLevel;
                break;

            case Difficulty.Normal:
                level.text = "Normal " + song.normalLevel;
                break;

            case Difficulty.Hard:
                level.text = "Hard " + song.hardLevel;
                break;

            case Difficulty.Master:
                level.text = "Master " + song.masterLevel;
                break;
        }               

        yield return new WaitForSeconds(4f);

        coverPanel.SetActive(false);

        music.clip = song.clip;

        music.Play();

        StartGame();
    }

    void StartGame()
    {
        Debug.Log("게임 시작");
    }
}
