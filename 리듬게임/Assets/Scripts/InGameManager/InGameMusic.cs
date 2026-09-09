using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMusic : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Song Cover")]
    [SerializeField] private GameObject songCover;

    [Header("Cover Time")]
    [SerializeField] private float coverTime = 2f;

    public float CurrentTime
    {
        get
        {
            if (audioSource == null)
                return 0f;

            return audioSource.time;
        }
    }

    public bool IsPlaying
    {
        get
        {
            if (audioSource == null)
                return false;

            return audioSource.isPlaying;
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (SongManager.instance == null)
            return;

        if (SongManager.instance.selectedSong == null)
            return;

        // 미리듣기 완전히 정지
        SongManager.instance.StopPreview();

        audioSource = GetComponent<AudioSource>();

        audioSource.Stop();
        audioSource.clip =
            SongManager.instance.selectedSong.clip;

        audioSource.playOnAwake = false;

        if (songCover != null)
            songCover.SetActive(true);

        Invoke(nameof(StartMusic), coverTime);
    }

    private void StartMusic()
    {
        if (songCover != null)
            songCover.SetActive(false);

        audioSource.time = 0f;
        audioSource.Play();
    }

    public void RestartGame()
    {
        StopAllCoroutines();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}