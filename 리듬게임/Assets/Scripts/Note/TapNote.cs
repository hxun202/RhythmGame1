using UnityEngine;

public class TapNote : BaseNote
{
    [Header("Note Movement")]
    [SerializeField] private float travelTime = 1.5f;

    [Header("Perspective")]
    [SerializeField] private float spawnScale = 0.35f;
    [SerializeField] private float judgeScale = 1f;

    private float spawnTime;

    public override void Initialize(
        NoteData data,
        MusicManager music,
        PoolManager pool,
        ScoreManager score)
    {
        base.Initialize(
            data,
            music,
            pool,
            score
        );

        spawnTime =
            data.time - travelTime;

        rectTransform.localScale =
            Vector3.one * spawnScale;
    }

    private void Update()
    {
        if (Data == null)
            return;

        Move();
        CheckMiss();
    }

    public override void Move()
    {
        float elapsed =
            musicManager.CurrentTime -
            spawnTime;

        float t =
            elapsed / travelTime;

        t = Mathf.Clamp01(t);

        // 위치
        rectTransform.anchoredPosition =
            Vector2.Lerp(
                spawnPosition,
                judgePosition,
                t
            );

        // 크기
        float scaleT =
            Mathf.Pow(t, 0.7f);

        float scale =
            Mathf.Lerp(
                spawnScale,
                judgeScale,
                scaleT
            );

        rectTransform.localScale =
            Vector3.one * scale;
    }

    protected override void CheckMiss()
    {
        if (judged)
            return;

        if (musicManager.CurrentTime >
            Data.time + 0.2f)
        {
            judged = true;

            scoreManager.AddJudge(
                JudgeResult.Miss
            );

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