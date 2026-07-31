using UnityEngine;
using System.Collections.Generic;

public class SongManager : MonoBehaviour
{
    public static SongManager instance;

    public MusicData selectedSong;
    public Difficulty selectedDifficulty;

    public Dictionary<MusicData, SongRecord> records = new Dictionary<MusicData, SongRecord>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("»ý¼º");
        }
        else
        {
            Debug.Log("ÆÄ±«");
            Destroy(gameObject);
        }
    }

    public SongRecord GetRecord(MusicData song)
    {
        if (!records.ContainsKey(song))
        {
            records[song] = new SongRecord();
        }

        return records[song];
    }
}
