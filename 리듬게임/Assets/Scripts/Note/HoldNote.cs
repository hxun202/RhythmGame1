using UnityEngine;

public class HoldNote : BaseNote
{
    [Header("Hold")]
    [SerializeField] private float spawnY = 800f;
    [SerializeField] private float judgeY = -350f;
    [SerializeField] private float travelTime = 1.5f;

    private float spawnTime;
    private float endTime;

    private bool holding;
    private bool judgedStart;

    public override void Initialize(
        NoteData data,
        MusicManager music,
        PoolManager pool,
        ScoreManager score)
    {
        base.Initialize(data, music, pool, score);

        spawnTime = data.time - travelTime;
        endTime = data.endTime;

        holding = false;
        judgedStart = false;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = spawnY;

        rectTransform.anchoredPosition = pos;
    }

    private void Update()
    {
        if (Data == null)
            return;

        Move();

        // 아직 홀드 시작 판정을 하지 않은 경우
        if (!judgedStart)
        {
            if (Input.GetKeyDown(GetLaneKey(Data.lane)))
            {
                Judge();
            }

            // 판정 시간을 놓침
            if (musicManager.CurrentTime > Data.time + 0.2f)
            {
                ReturnPool();
            }

            return;
        }

        // 홀드 중
        if (holding)
        {
            // 키를 떼면 실패
            if (!Input.GetKey(GetLaneKey(Data.lane)))
            {
                holding = false;
                ReturnPool();
                return;
            }

            // 홀드 끝까지 유지
            if (musicManager.CurrentTime >= endTime)
            {
                holding = false;

                // 나중에 점수 처리

                ReturnPool();
            }
        }
    }

    public override void Move()
    {
        float progress =
            (musicManager.CurrentTime - spawnTime) / travelTime;

        Vector2 pos = rectTransform.anchoredPosition;

        pos.y = Mathf.Lerp(
            spawnY,
            judgeY,
            progress
        );

        rectTransform.anchoredPosition = pos;
    }

    public override void Judge()
    {
        judgedStart = true;
        holding = true;
    }

    private KeyCode GetLaneKey(int lane)
    {
        switch (lane)
        {
            case 0:
                return KeyCode.A;

            case 1:
                return KeyCode.S;

            case 2:
                return KeyCode.D;

            case 3:
                return KeyCode.J;

            case 4:
                return KeyCode.K;

            case 5:
                return KeyCode.L;
        }

        return KeyCode.None;
    }
}