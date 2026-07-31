//using UnityEngine;
//using System.Collections.Generic;

//public class NoteSpawner : MonoBehaviour
//{
//    [SerializeField] private ChartManager chartManager;
//    [SerializeField] private MusicManager musicManager;

//    [SerializeField] private GameObject notePrefab;

//    [SerializeField] private Transform[] lanes;

//    [SerializeField]
//    private float travelTime = 2f;

//    private readonly List<BaseNote> activeNotes = new();

//    private int currentIndex = 0;

//    private Vector3 judgePosition;

//    private void Start()
//    {
//        judgePosition = new Vector3(0, -300, 0);
//    }

//    private void Update()
//    {
//        SpawnNotes();

//        foreach (var note in activeNotes)
//        {
//            note.Move(musicManager.CurrentTime);
//        }
//    }

//    private void SpawnNotes()
//    {
//        if (currentIndex >= chartManager.Chart.notes.Count)
//            return;

//        float currentTime = musicManager.CurrentTime;

//        while (currentIndex < chartManager.Chart.notes.Count)
//        {
//            var data = chartManager.Chart.notes[currentIndex];

//            if (currentTime >= data.time - travelTime)
//            {
//                Spawn(data);

//                currentIndex++;
//            }
//            else
//            {
//                break;
//            }
//        }
//    }

//    private void Spawn(NoteData data)
//    {
//        GameObject obj =
//            Instantiate(notePrefab);

//        Vector3 spawn =
//            lanes[data.lane].position;

//        Vector3 target =
//            new Vector3(
//                spawn.x,
//                judgePosition.y,
//                0);

//        BaseNote note =
//            obj.GetComponent<BaseNote>();

//        note.Initialize(
//            data,
//            musicManager);

//        activeNotes.Add(note);
//    }
//}