using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SongManager : MonoBehaviour
{
    public static SongManager instance;

    public MusicData selectedSong;
    public Difficulty selectedDifficulty;

    public Dictionary<MusicData, SongRecord> records =
        new Dictionary<MusicData, SongRecord>();

   private AudioSource previewSource;

   public GameResult lastResult = new GameResult();

    [SerializeField] private AudioMixerGroup previewOutput;

    [System.Serializable]
    public class GameResult
    {
        public int score;
        public int maxCombo;

        public int perfect;
        public int great;
        public int good;
        public int miss;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            previewSource = GetComponent<AudioSource>();

            if (previewSource == null)
            {
                previewSource = gameObject.AddComponent<AudioSource>();
            }

            previewSource.playOnAwake = false;
            previewSource.loop = true;

            previewSource.outputAudioMixerGroup = previewOutput;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPreview(MusicData song)
    {
        if (song == null || song.clip == null)
            return;

        selectedSong = song;

        previewSource.clip = song.clip;
        previewSource.Play();
    }

    public float GetPreviewTime()
    {
        if (previewSource == null)
            return 0f;

        return previewSource.time;
    }

    public float GetPreviewLength()
    {
        if (previewSource == null ||
            previewSource.clip == null)
            return 0f;

        return previewSource.clip.length;
    }

    public void StopPreview()
    {
        if (previewSource == null)
            return;

        previewSource.Stop();
        previewSource.clip = null;
    }

    public SongRecord GetRecord(MusicData song)
    {
        if (!records.ContainsKey(song))
        {
            records[song] = new SongRecord();
        }

        return records[song];
    }

    public void SaveGameResult(ScoreManager scoreManager)
    {
        if (scoreManager == null)
            return;

        lastResult.score = scoreManager.Score;
        lastResult.maxCombo = scoreManager.MaxCombo;

        lastResult.perfect = scoreManager.PerfectCount;
        lastResult.great = scoreManager.GreatCount;
        lastResult.good = scoreManager.GoodCount;
        lastResult.miss = scoreManager.MissCount;
    }

    public string GetRank()
    {
        int total =
            lastResult.perfect +
            lastResult.great +
            lastResult.good +
            lastResult.miss;

        if (total == 0)
            return "D";

        float accuracy =
            (lastResult.perfect +
            lastResult.great * 0.8f +
            lastResult.good * 0.5f) / total;

        if (accuracy >= 0.90f)
            return "S";

        if (accuracy >= 0.80f)
            return "A";

        if (accuracy >= 0.70f)
            return "B";

        if (accuracy >= 0.60f)
            return "C";

        return "D";
    }
}