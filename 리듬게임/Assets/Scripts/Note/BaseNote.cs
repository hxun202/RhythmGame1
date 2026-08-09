using UnityEngine;
using System;

public abstract class BaseNote : MonoBehaviour
{
    protected PoolManager poolManager;
    protected MusicManager musicManager;
    protected ScoreManager scoreManager;

    protected RectTransform rectTransform;

    protected bool judged;

    public event Action<BaseNote> OnReturned;

    public NoteData Data { get; private set; }

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

        if (musicManager.CurrentTime >Data.time + 0.200f)
        {
            judged = true;

            scoreManager.AddJudge(JudgeResult.Miss);

            ReturnPool();
        }
    }

    public virtual void ReturnPool()
    {
        gameObject.SetActive(false);

        poolManager.Return(Data.type, gameObject);
    }
}