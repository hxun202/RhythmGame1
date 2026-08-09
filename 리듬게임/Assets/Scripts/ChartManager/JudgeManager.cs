using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private NoteSpawner noteSpawner;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Input")]
    [SerializeField]
    private KeyCode[] laneKeys =
    {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L
    };

    [Header("Judge Window")]
    [SerializeField] private float perfectWindow = 0.050f;
    [SerializeField] private float greatWindow = 0.100f;
    [SerializeField] private float goodWindow = 0.150f;
    [SerializeField] private float missWindow = 0.200f;

    private void Update()
    {
        for (int lane = 0; lane < laneKeys.Length; lane++)
        {
            if (Input.GetKeyDown(laneKeys[lane]))
            {
                TryJudge(lane);
            }
        }
    }

    private void TryJudge(int lane)
    {
        BaseNote note = noteSpawner.GetClosestNote(lane);

        if (note == null)
            return;

        float difference = Mathf.Abs(
            note.NoteTime - musicManager.CurrentTime
        );

        if (difference <= perfectWindow)
        {
            ProcessJudge(note, JudgeResult.Perfect);
        }
        else if (difference <= greatWindow)
        {
            ProcessJudge(note, JudgeResult.Great);
        }
        else if (difference <= goodWindow)
        {
            ProcessJudge(note, JudgeResult.Good);
        }
        else if (difference <= missWindow)
        {
            ProcessJudge(note, JudgeResult.Miss);
        }
    }

    private void ProcessJudge
        (BaseNote note,
        JudgeResult result)
    {
        scoreManager.AddJudge(result);

        note.Judge();
    }
}