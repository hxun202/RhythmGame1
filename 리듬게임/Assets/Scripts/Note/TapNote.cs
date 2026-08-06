using UnityEngine;

public class TapNote : BaseNote
{
    [SerializeField]
    private float spawnY = 800f;

    [SerializeField]
    private float judgeY = -400f;

    [SerializeField]
    private float travelTime = 2f;

    private float spawnTime;

    public override void Initialize(
        NoteData data,
        MusicManager music,
        PoolManager pool)
    {
        base.Initialize(data, music, pool);

        spawnTime = data.time - travelTime;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = spawnY;
        rectTransform.anchoredPosition = pos;
    }

    private void Update()
    {
        if (Data == null)
            return;

        Move();
    }

    public override void Move()
    {
        float t =
            (musicManager.CurrentTime - spawnTime)
            / travelTime;

        Vector2 pos =
            rectTransform.anchoredPosition;

        pos.y =
            Mathf.Lerp(spawnY, judgeY, t);

        rectTransform.anchoredPosition = pos;

        if (t > 1.2f)
        {
            ReturnPool();
        }
    }

    public override void Judge()
    {

    }
}