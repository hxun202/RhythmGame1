using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class BaseNote : MonoBehaviour
{
    protected PoolManager poolManager;
    protected MusicManager musicManager;
    protected ScoreManager scoreManager;

    protected RectTransform rectTransform;

    protected bool judged;

    protected Vector2 spawnPosition;
    protected Vector2 judgePosition;

    public event Action<BaseNote> OnReturned;

    public NoteData Data { get; private set; }

    public bool IsJudged
    {
        get
        {
            return judged;
        }
    }

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Initialize(
        NoteData data,
        MusicManager music,
        PoolManager pool,
        ScoreManager score)
    {
        Data = data;

        musicManager = music;
        poolManager = pool;
        scoreManager = score;

        judged = false;

        gameObject.SetActive(true);
    }

    public void SetMovementPosition(
        Vector2 spawn,
        Vector2 judge)
    {
        spawnPosition = spawn;
        judgePosition = judge;
    }

    public float NoteTime
    {
        get
        {
            return Data.time;
        }
    }

    protected void InvokeReturned()
    {
        OnReturned?.Invoke(this);
    }

    public abstract void Move();

    public abstract void Judge();

    protected virtual void CheckMiss()
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

    public virtual void ReturnPool()
    {
        InvokeReturned();

        gameObject.SetActive(false);

        poolManager.Return(
            Data.type,
            gameObject
        );
    }
}