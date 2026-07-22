using UnityEngine;

public class SongData : MonoBehaviour
{
    public string songName;
    public string composer;

    public string easyLevel;
    public string normalLevel;
    public string hardLevel;
    public string masterLevel;

    public AudioClip clip;
    public Sprite image;

    public SongList songList;

    public void ClickSong()
    {
        songList.ShowSong(this);
    }
}
