using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private MusicManager musicManager;

    [SerializeField] private ChartManager chartManager;

    private void Start()
    {
        chartManager.LoadChart("test");

        musicManager.PlayMusic();
    }
}
