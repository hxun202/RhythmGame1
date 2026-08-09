using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int perfectScore = 800;
    [SerializeField] private int greatScore = 400;
    [SerializeField] private int goodScore = 200;

    public int Score { get; private set; }
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }

    public void AddJudge(JudgeResult result)
    {
        switch (result)
        {
            case JudgeResult.Perfect:
                AddScore(perfectScore);
                ComboUp();
                break;

            case JudgeResult.Great:
                AddScore(greatScore);
                ComboUp();
                break;

            case JudgeResult.Good:
                AddScore(goodScore);
                ComboUp();
                break;

            case JudgeResult.Miss:
                ComboReset();
                break;
        }

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
}