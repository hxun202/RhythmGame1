using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicData : ScriptableObject
{
    public string songName;
    public string composer;

    public string easyLevel;
    public string normalLevel;
    public string hardLevel;
    public string masterLevel;

    public AudioClip clip;
    public Sprite image;
}
