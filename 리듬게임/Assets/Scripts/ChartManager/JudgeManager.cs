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
        BaseNote note =
            noteSpawner.GetClosestNote(lane);

        if (note == null)
            return;

        float difference =
            Mathf.Abs(
                note.NoteTime -
                musicManager.CurrentTime
            );

        JudgeResult result;

        if (difference <= perfectWindow)
        {
            result = JudgeResult.Perfect;
        }
        else if (difference <= greatWindow)
        {
            result = JudgeResult.Great;
        }
        else if (difference <= goodWindow)
        {
            result = JudgeResult.Good;
        }
        else
        {
            return;
        }

        // 점수와 콤보는 여기서 딱 한 번
        scoreManager.AddJudge(result);

        // 노트 상태만 변경
        note.Judge();
    }
}