using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongList : MonoBehaviour
{
    public MusicData[] songs;

    public Image albumImage;

    public TMP_Text songNameText;
    public TMP_Text composerText;

    public GameObject songDetailsPanel;

    [Header("Song Time")]
    [SerializeField] private TMP_Text songTimeStart;
    [SerializeField] private TMP_Text songTimeEnd;

    public void ShowSong(MusicData song)
    {
        if (SongManager.instance == null)
            return;

        if (song == null)
            return;

        SongManager.instance.PlayPreview(song);

        songDetailsPanel.SetActive(true);

        albumImage.sprite = song.image;
        songNameText.text = song.songName;
        composerText.text = song.composer;
    }

    private void Update()
    {
        if (SongManager.instance == null)
            return;

        if (songTimeStart == null ||
            songTimeEnd == null)
            return;

        float currentTime =
            SongManager.instance.GetPreviewTime();

        float totalTime =
            SongManager.instance.GetPreviewLength();

        songTimeStart.text =
            FormatTime(currentTime);

        songTimeEnd.text =
            FormatTime(totalTime);
    }

    private string FormatTime(float time)
    {
        int minutes =
            Mathf.FloorToInt(time / 60f);

        int seconds =
            Mathf.FloorToInt(time % 60f);

        return $"{minutes}:{seconds:00}";
    }
}