using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StopManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject stopPanel;
    [SerializeField] private TMP_Text countText;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    private bool isPaused = false;
    private bool isCounting = false;

    private void Start()
    {
        stopPanel.SetActive(false);
        countText.gameObject.SetActive(false);
    }

    // 정지 버튼
    public void OpenStopPanel()
    {
        if (isPaused || isCounting)
            return;

        isPaused = true;

        if (musicSource != null)
            musicSource.Pause();

        stopPanel.SetActive(!stopPanel.activeSelf);
    }

    // 계속하기 버튼
    public void ContinueGame()
    {
        if (!isPaused || isCounting)
            return;

        StartCoroutine(ResumeGame());
    }

    private IEnumerator ResumeGame()
    {
        isCounting = true;

        // 패널 닫기
        stopPanel.SetActive(false);

        // 숫자 표시
        countText.gameObject.SetActive(true);

        countText.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        countText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        countText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        // 숫자 숨기기
        countText.gameObject.SetActive(false);

        // 음악 다시 재생
        if (musicSource != null)
            musicSource.UnPause();

        isPaused = false;
        isCounting = false;
    }

    // 처음부터 다시
    public void RestartGame()
    {
        StopAllCoroutines();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    // 로비로 나가기
    public void GoToLobby()
    {
        StopAllCoroutines();

        if (musicSource != null)
            musicSource.Stop();

        if (SongManager.instance != null)
            SongManager.instance.StopPreview();

        SceneManager.LoadScene("Lobby");
    }
}