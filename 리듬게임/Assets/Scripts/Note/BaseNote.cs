using UnityEngine;

public abstract class BaseNote : MonoBehaviour
{
    public NoteData Data { get; private set; }

    protected MusicManager musicManager;

    public virtual void Initialize(
        NoteData data,
        MusicManager music)
    {
        Data = data;
        musicManager = music;

        gameObject.SetActive(true);
    }

    public abstract void Move();

    public abstract void Judge();

    public virtual void ReturnPool()
    {
        gameObject.SetActive(false);
    }
}