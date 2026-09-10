using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class InGameMusic : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private SongCover songCover;

    [Header("Game Finish")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TMP_Text finishText;

    private AudioSource audioSource;

    private bool musicStarted = false;
    private bool resultLoaded = false;

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

    private void Start()
    {
        if (songCover == null)
        {
            Debug.LogError("InGameMusic: SongCover가 연결되지 않았습니다.");
            return;
        }

        audioSource = songCover.music;

        if (audioSource == null)
        {
            Debug.LogError("InGameMusic: SongCover의 Music AudioSource가 없습니다.");
            return;
        }

        if (finishText != null)
            finishText.gameObject.SetActive(false);

        Debug.Log("InGameMusic: SongCover의 음악을 감지하기 시작");
    }

    private void Update()
    {
        if (audioSource == null)
            return;

        if (audioSource.clip == null)
            return;

        // 아직 음악이 시작되지 않았으면 기다림
        if (!musicStarted)
        {
            if (audioSource.isPlaying)
            {
                musicStarted = true;

                Debug.Log("InGameMusic: 음악 시작 감지");
            }

            return;
        }

        if (resultLoaded)
            return;

        // 음악이 끝났는지 확인
        if (!audioSource.isPlaying &&
            audioSource.time >= audioSource.clip.length - 0.2f)
        {
            resultLoaded = true;

            StartCoroutine(FinishGame());
        }
    }

    private IEnumerator FinishGame()
    {
        Debug.Log("곡 종료 → 결과 처리 시작");

        audioSource.Stop();

        // 결과 저장
        if (SongManager.instance != null &&
            scoreManager != null)
        {
            SongManager.instance.SaveGameResult(scoreManager);

            Debug.Log("게임 결과 저장 완료");
        }
        else
        {
            Debug.LogError("ScoreManager 또는 SongManager가 없습니다.");
        }

        // Live Finish 표시
        if (finishText != null)
        {
            finishText.text = "Live Finish!";
            finishText.gameObject.SetActive(true);

            Debug.Log("Live Finish 표시");
        }
        else
        {
            Debug.LogError("Finish Text가 연결되지 않았습니다.");
        }

        yield return new WaitForSecondsRealtime(2f);

        Debug.Log("GameFinish 씬으로 이동");

        SceneManager.LoadScene("GameFinish");
    }

    public void RestartGame()
    {
        StopAllCoroutines();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}