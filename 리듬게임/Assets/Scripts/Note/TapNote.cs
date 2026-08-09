using UnityEngine;

public class TapNote : BaseNote
{
    [SerializeField] private float spawnY = 800f;
    [SerializeField] private float judgeY = -350f;
    [SerializeField] private float travelTime = 1.5f;
   // [SerializeField] private float missDistance = 120f;

    private float spawnTime;

    public override void Initialize(
        NoteData data,
        MusicManager music,
        PoolManager pool,
        ScoreManager score)
    {
        base.Initialize(data, music, pool, score);

        spawnTime = data.time - travelTime;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = spawnY;
        rectTransform.anchoredPosition = pos;
    }

    private void Update()
    {
        if (Data == null)
        {
            return;
        }

        Move();
        CheckMiss();
    }

    public override void Move()
    {
        float elapsed = musicManager.CurrentTime - spawnTime;
        float t = elapsed / travelTime;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = Mathf.Lerp(spawnY, judgeY, t);
        rectTransform.anchoredPosition = pos;
    }

    protected override void CheckMiss()
    {
        if (judged)
        {
            return;
        }

        if (musicManager.CurrentTime > Data.time + 0.2f)
        {
            judged = true;

            scoreManager.AddJudge(JudgeResult.Miss);

            ReturnPool();
        }
    }

    public override void Judge()
    {
        if (judged)
        {
            return;
        }

        judged = true;

        ReturnPool();
    }
}