using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioSource bgmSource;
    public AudioSource previewSource;

    public void SelectSong(AudioClip clip)
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }

        previewSource.clip = clip;
        previewSource.Play();
      //  DontDestroyOnLoad(previewSource);
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
