using UnityEngine;

public class SongManager : MonoBehaviour
{
    public static SongManager instance;

    public SongData selectedSong;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
