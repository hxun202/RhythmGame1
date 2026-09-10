using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int perfectScore = 800;
    [SerializeField] private int greatScore = 400;
    [SerializeField] private int goodScore = 100;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private TMP_Text judgeText;

    [Header("Judge UI")]
    [SerializeField] private float judgeDisplayTime = 0.5f;

    public int PerfectCount { get; private set; }
    public int GreatCount { get; private set; }
    public int GoodCount { get; private set; }
    public int MissCount { get; private set; }

    public int Score { get; private set; }
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }

    private Coroutine judgeCoroutine;

    private void Start()
    {
        Score = 0;
        Combo = 0;
        MaxCombo = 0;

        PerfectCount = 0;
        GreatCount = 0;
        GoodCount = 0;
        MissCount = 0;

        UpdateScoreUI();
        UpdateComboUI();

        if (judgeText != null)
        {
            judgeText.text = "";
        }
    }

    public void AddJudge(JudgeResult result)
    {
        switch (result)
        {
            case JudgeResult.Perfect:
                PerfectCount++;
                AddScore(perfectScore);
                ComboUp();
                break;

            case JudgeResult.Great:
                GreatCount++;
                AddScore(greatScore);
                ComboUp();
                break;

            case JudgeResult.Good:
                GoodCount++;
                ComboReset();
                break;

            case JudgeResult.Miss:
                MissCount++;
                ComboReset();
                break;
        }

        UpdateScoreUI();
        UpdateComboUI();
        ShowJudge(result);

        Debug.Log(
            $"Judge: {result} | Score: {Score} | Combo: {Combo}"
        );
    }

    private void AddScore(int amount)
    {
        Score += amount;
    }

    private void ComboUp()
    {
        Combo++;

        if (Combo > MaxCombo)
        {
            MaxCombo = Combo;
        }
    }

    private void ComboReset()
    {
        Combo = 0;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = Score.ToString("D7");
        }
    }

    private void UpdateComboUI()
    {
        if (comboText != null)
        {
            comboText.text = Combo.ToString("D3");
        }
    }

    private void ShowJudge(JudgeResult result)
    {
        if (judgeText == null)
        {
            return;
        }

        // 이전 판정 표시 중이면 코루틴 중지
        if (judgeCoroutine != null)
        {
            StopCoroutine(judgeCoroutine);
        }

        // 판정 표시
        judgeText.text = result.ToString().ToUpper();

        // 일정 시간 후 지우기
        judgeCoroutine = StartCoroutine(HideJudge());
    }

    private IEnumerator HideJudge()
    {
        yield return new WaitForSeconds(judgeDisplayTime);

        if (judgeText != null)
        {
            judgeText.text = "";
        }

        judgeCoroutine = null;
    }
}