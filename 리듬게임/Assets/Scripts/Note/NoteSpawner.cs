using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ChartManager chartManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Note Parent")]
    [SerializeField] private RectTransform noteParent;

    [Header("Lane")]
    [SerializeField] private Transform[] lanes;

    [Header("Note Movement")]
    [SerializeField] private float spawnY = 600f;
    [SerializeField] private float judgeY = -230f;

    [Header("Lane Spawn X")]
    [SerializeField]
    private float[] spawnX =
    {
        -80f,
        -40f,
        -20f,
         20f,
         40f,
         80f
    };

    [Header("Lane Judge X")]
    [SerializeField]
    private float[] judgeX =
    {
        -700f,
        -400f,
        -130f,
         130f,
         400f,
         700f
    };

    private int currentIndex = 0;

    private readonly List<BaseNote> activeNotes =
        new List<BaseNote>();

    private void Update()
    {
        SpawnNote();
    }

    private void SpawnNote()
    {
        if (chartManager == null)
            return;

        if (musicManager == null)
            return;

        if (poolManager == null)
            return;

        if (scoreManager == null)
            return;

        if (!musicManager.IsPlaying)
            return;

        if (chartManager.Chart == null)
            return;

        if (currentIndex >= chartManager.Chart.notes.Count)
            return;

        NoteData data =
            chartManager.Chart.notes[currentIndex];

        float spawnTime =
            data.time - 1.5f;

        if (musicManager.CurrentTime >= spawnTime)
        {
            CreateNote(data);

            currentIndex++;
        }
    }

    private void CreateNote(NoteData data)
    {
        GameObject obj =
            poolManager.Get(data.type);

        RectTransform noteRect =
            obj.GetComponent<RectTransform>();

        noteRect.SetParent(
            noteParent,
            false
        );

        Vector2 spawnPosition =
            new Vector2(
                spawnX[data.lane],
                spawnY
            );

        Vector2 judgePosition =
            new Vector2(
                judgeX[data.lane],
                judgeY
            );

        noteRect.anchoredPosition =
            spawnPosition;

        BaseNote note =
            obj.GetComponent<BaseNote>();

        note.Initialize(
            data,
            musicManager,
            poolManager,
            scoreManager
        );

        note.SetMovementPosition(
            spawnPosition,
            judgePosition
        );

        activeNotes.Add(note);
    }

    public BaseNote GetClosestNote(int lane)
    {
        BaseNote closest = null;

        float closestDifference =
            float.MaxValue;

        foreach (BaseNote note in activeNotes)
        {
            if (note == null)
                continue;

            if (!note.gameObject.activeSelf)
                continue;

            if (note.Data == null)
                continue;

            if (note.IsJudged)
                continue;

            if (note.Data.lane != lane)
                continue;

            float difference =
                Mathf.Abs(
                    note.NoteTime -
                    musicManager.CurrentTime
                );

            if (difference < closestDifference)
            {
                closestDifference = difference;
                closest = note;
            }
        }

        return closest;
    }

    public void HandleNoteReturned(BaseNote note)
    {
        if (note == null)
            return;

        activeNotes.Remove(note);
    }
}