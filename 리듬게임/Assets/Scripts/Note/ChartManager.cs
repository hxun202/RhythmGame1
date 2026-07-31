using UnityEngine;
using Newtonsoft.Json;

public class ChartManager : MonoBehaviour
{
    public ChartData Chart { get; private set; }

    private void Awake()
    {
        LoadChart("test");
    }

    public void LoadChart(string chartName)
    {
        TextAsset chart =
            Resources.Load<TextAsset>($"Charts/{chartName}");

        if (chart == null)
        {
            Debug.LogError($"{chartName} 채보를 찾을 수 없습니다.");
            return;
        }

        Chart = JsonConvert.DeserializeObject<ChartData>(chart.text);

        Debug.Log($"곡 이름 : {Chart.songName}");
        Debug.Log($"노트 개수 : {Chart.notes.Count}");
    }
}