using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicData : ScriptableObject
{
    public string songName;
    public string composer;

    public AudioClip clip;
    public Sprite image;

    public SongList songList;
}
