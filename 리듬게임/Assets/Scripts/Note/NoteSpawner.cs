using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ChartManager chartManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private RectTransform NoteParent;

    [Header("Lane")]
    [SerializeField] private RectTransform[] lanes;

    private readonly List<BaseNote> activeNotes = new();

    private int currentIndex = 0;

    private void Update()
    {
        SpawnNote();
    }

    private void SpawnNote()
    {
        if (chartManager.Chart == null)
            return;

        if (currentIndex >= chartManager.Chart.notes.Count)
            return;

        NoteData data = chartManager.Chart.notes[currentIndex];

        float spawnTime = data.time - 2f;

        if (musicManager.CurrentTime >= spawnTime)
        {
            CreateNote(data);

            currentIndex++;
        }
    }

    private void CreateNote(NoteData data)
    {
        GameObject obj = poolManager.Get(data.type);

        RectTransform noteRect = obj.GetComponent<RectTransform>();

        noteRect.SetParent(NoteParent, false);   // Áß¿ä

        noteRect.anchoredPosition = new Vector2(
            lanes[data.lane].anchoredPosition.x,
            600f
        );

        BaseNote note = obj.GetComponent<BaseNote>();

        note.OnReturned += HandleNoteReturned;
        note.OnReturned -= HandleNoteReturned;

        note.Initialize
            (data,
            musicManager,
            poolManager,
            scoreManager);

        activeNotes.Add(note);
    }

    private void HandleNoteReturned(BaseNote note)
    {
        activeNotes.Remove(note);

        note.OnReturned -= HandleNoteReturned;
    }

    public BaseNote GetClosestNote(int lane)
    {
        BaseNote closest = null;
        float closestDifference = float.MaxValue;

        foreach (BaseNote note in activeNotes)
        {
            if (note.Data.lane != lane)
                continue;

            float difference =
                Mathf.Abs(note.NoteTime - musicManager.CurrentTime);

            if (difference < closestDifference)
            {
                closestDifference = difference;
                closest = note;
            }
        }

        return closest;
    }
}