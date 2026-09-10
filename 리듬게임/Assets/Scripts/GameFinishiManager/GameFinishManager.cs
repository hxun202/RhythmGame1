using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SongManager;

public class GameFinishManager : MonoBehaviour
{
    [Header("Result Text")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private TMP_Text rankText;

    [Header("Judge Count")]
    [SerializeField] private TMP_Text perfectText;
    [SerializeField] private TMP_Text greatText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text missText;

    private void Start()
    {
        if (SongManager.instance == null)
            return;

        SongManager manager = SongManager.instance;
        GameResult result = manager.lastResult;

        scoreText.text = result.score.ToString();
        comboText.text = result.maxCombo.ToString();
        rankText.text = manager.GetRank();

        perfectText.text = result.perfect.ToString();
        greatText.text = result.great.ToString();
        goodText.text = result.good.ToString();
        missText.text = result.miss.ToString();
    }

    public void Next()
    {
        SceneManager.LoadScene("Lobby");
    }
}