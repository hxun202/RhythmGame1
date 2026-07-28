using UnityEngine;
using System.Collections.Generic;

public class SongManager : MonoBehaviour
{
    public static SongManager instance;

    public SongData selectedSong;
    public Difficulty selectedDifficulty;

    public Dictionary<SongData, SongRecord> records = new Dictionary<SongData, SongRecord>();

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

    public SongRecord GetRecord(SongData song)
    {
        if (!records.ContainsKey(song))
        {
            records[song] = new SongRecord();
        }

        return records[song];
    }
}
