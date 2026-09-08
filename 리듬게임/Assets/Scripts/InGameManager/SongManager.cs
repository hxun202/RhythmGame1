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
    [SerializeField] private AudioMixerGroup previewOutput;

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
}