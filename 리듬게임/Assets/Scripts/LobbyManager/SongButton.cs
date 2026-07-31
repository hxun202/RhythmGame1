using UnityEngine;

public class SongButton : MonoBehaviour
{
    public MusicData music;

    public SongList songList;

    public void ClickSong()
    {
        songList.ShowSong(music);
    }
}