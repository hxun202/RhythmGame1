using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioSource bgmSource;
    public AudioSource previewSource;

    public AudioClip song1;

    public void SelectSong(AudioClip clip)
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }

        previewSource.Stop();

        previewSource.clip = clip;
        previewSource.Play();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void BackToLobby()
    {
        previewSource.Stop();

        bgmSource.Play();
    }

}
