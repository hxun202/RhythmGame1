using UnityEngine;

public class HoldNote : BaseNote
{
    [Header("Hold Parts")]
    [SerializeField] private RectTransform head;
    [SerializeField] private RectTransform body;
    [SerializeField] private RectTransform tail;

    [Header("Movement")]
    [SerializeField] private float travelTime = 1.5f;

    [Header("Size")]
    [SerializeField] private float headSize = 140f;
    [SerializeField] private float tailSize = 140f;
    [SerializeField] private float bodyWidth = 150f;

    private float spawnTime;
    private float endTime;

    private float holdDuration;

    private bool holding;
    private bool judgedStart;

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

        endTime =
            data.endTime;

        holdDuration =
            endTime - data.time;

        holding = false;
        judgedStart = false;
        judged = false;

        ResetVisual();
    }

    private void Update()
    {
        if (Data == null)
            return;

        Move();

        // 아직 시작 판정을 받지 못한 상태
        if (!judgedStart)
        {
            // JudgeManager가 판정하기 때문에
            // 여기서는 입력을 검사하지 않는다.

            // 시작 시간을 놓쳤으면
            // 롱노트 전체 제거
            if (musicManager.CurrentTime >
                Data.time + 0.2f)
            {
                judged = true;
                judgedStart = true;

                scoreManager.AddJudge(
                    JudgeResult.Miss
                );

                ReturnPool();
            }

            return;
        }

        // 홀드 중
        if (holding)
        {
            if (!Input.GetKey(
                GetLaneKey(Data.lane)))
            {
                holding = false;

                scoreManager.AddJudge(
                    JudgeResult.Miss
                );

                ReturnPool();

                return;
            }

            // 끝까지 누름
            if (musicManager.CurrentTime >= endTime)
            {
                holding = false;

                scoreManager.AddJudge(
                    JudgeResult.Perfect
                );

                ReturnPool();

                return;
            }
        }
    }

    public override void Move()
    {
        float currentTime =
            musicManager.CurrentTime;

        Vector2 headPosition =
            GetPositionAtTime(currentTime);

        float tailTime =
            currentTime - holdDuration;

        bool tailVisible =
            tailTime >= spawnTime;

        Vector2 tailPosition;

        if (tailVisible)
        {
            tailPosition =
                GetPositionAtTime(tailTime);
        }
        else
        {
            tailPosition =
                spawnPosition;
        }

        // 머리가 현재 위치로 이동
        rectTransform.anchoredPosition =
            headPosition;

        // -------------------------
        // Head
        // -------------------------
        if (head != null)
        {
            head.anchoredPosition =
                Vector2.zero;
        }

        // -------------------------
        // Body
        // -------------------------
        if (body != null)
        {
            Vector2 direction =
                tailPosition - headPosition;

            float length =
                direction.magnitude;

            body.anchoredPosition =
                direction * 0.5f;

            body.sizeDelta =
                new Vector2(
                    bodyWidth,
                    length
                );

            float angle =
                Mathf.Atan2(
                    direction.y,
                    direction.x
                ) * Mathf.Rad2Deg;

            body.localRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    angle - 90f
                );

            body.gameObject.SetActive(
                length > 0.1f
            );
        }

        // -------------------------
        // Tail
        // -------------------------
        if (tail != null)
        {
            if (tailVisible)
            {
                tail.gameObject.SetActive(true);

                tail.anchoredPosition =
                    tailPosition - headPosition;
            }
            else
            {
                tail.gameObject.SetActive(false);
            }
        }
    }

    private Vector2 GetPositionAtTime(float time)
    {
        float progress =
            (time - spawnTime) /
            travelTime;

        progress =
            Mathf.Clamp01(progress);

        return Vector2.Lerp(
            spawnPosition,
            judgePosition,
            progress
        );
    }

    public override void Judge()
    {
        if (judgedStart)
            return;

        judgedStart = true;
        holding = true;

        Debug.Log("Hold Start");
    }

    private void ResetVisual()
    {
        if (head != null)
        {
            head.anchoredPosition =
                Vector2.zero;

            head.localRotation =
                Quaternion.identity;

            head.sizeDelta =
                new Vector2(
                    headSize,
                    headSize
                );
        }

        if (body != null)
        {
            body.anchoredPosition =
                Vector2.zero;

            body.localRotation =
                Quaternion.identity;

            body.sizeDelta =
                new Vector2(
                    bodyWidth,
                    0f
                );
        }

        if (tail != null)
        {
            tail.anchoredPosition =
                Vector2.zero;

            tail.localRotation =
                Quaternion.identity;

            tail.sizeDelta =
                new Vector2(
                    tailSize,
                    tailSize
                );
        }
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