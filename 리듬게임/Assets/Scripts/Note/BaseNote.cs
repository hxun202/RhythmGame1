using UnityEngine;

public abstract class BaseNote : MonoBehaviour
{
    protected RectTransform rectTransform;

    protected PoolManager poolManager;
    protected MusicManager musicManager;

    public NoteData Data { get; private set; }

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Initialize(
        NoteData data,
        MusicManager music,
        PoolManager pool)
    {
        Data = data;
        musicManager = music;
        poolManager = pool;

        gameObject.SetActive(true);
    }

    public abstract void Move();

    public abstract void Judge();

    public virtual void ReturnPool()
    {
        poolManager.Return(Data.type, gameObject);
    }
}