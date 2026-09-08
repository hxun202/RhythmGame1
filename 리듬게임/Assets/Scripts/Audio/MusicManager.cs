using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource inGameMusic;

    public float CurrentTime
    {
        get
        {
            if (inGameMusic == null)
                return 0f;

            return inGameMusic.time;
        }
    }

    public bool IsPlaying
    {
        get
        {
            if (inGameMusic == null)
                return false;

            return inGameMusic.isPlaying;
        }
    }

    public void PlayMusic()
    {
        Debug.Log("음악은 InGameMusic에서 재생합니다.");
    }

    public void PauseMusic()
    {
        Debug.Log("Pause는 나중에 연결");
    }

    public void StopMusic()
    {
        Debug.Log("Stop은 나중에 연결");
    }
}