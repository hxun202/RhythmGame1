using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    public float CurrentTime => musicSource.time;

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
