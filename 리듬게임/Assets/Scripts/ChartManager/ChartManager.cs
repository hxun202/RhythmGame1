using UnityEngine;
using Newtonsoft.Json;

public class ChartManager : MonoBehaviour
{
    public ChartData Chart { get; private set; }

    private void Awake()
    {
        LoadChart("holdtest");
    }

    public void LoadChart(string chartName)
    {
        TextAsset chart =
            Resources.Load<TextAsset>($"Charts/{chartName}");

        if (chart == null)
        {
            Debug.LogError(
                $"{chartName} 채보를 찾을 수 없습니다."
            );

            return;
        }

        try
        {
            Chart =
                JsonConvert.DeserializeObject<ChartData>(
                    chart.text
                );
        }
        catch (JsonException exception)
        {
            Debug.LogError(
                $"채보 JSON 파싱 실패: {exception.Message}"
            );

            Chart = null;
            return;
        }

        if (Chart == null)
        {
            Debug.LogError("채보 데이터가 null입니다.");
            return;
        }

        if (Chart.notes == null)
        {
            Debug.LogError("채보의 notes가 null입니다.");
            return;
        }

        Debug.Log($"곡 이름 : {Chart.songName}");
        Debug.Log($"노트 개수 : {Chart.notes.Count}");
    }
}