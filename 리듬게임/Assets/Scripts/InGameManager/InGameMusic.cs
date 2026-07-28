using UnityEngine;

public class InGameMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (SongManager.instance == null)
        {
            return;
        }

        if (SongManager.instance.selectedSong == null)
        {
            return;
        }

        audioSource.clip = SongManager.instance.selectedSong.clip;
        audioSource.Play();
    }
}