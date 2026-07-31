using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class ChartData 
{
    [JsonProperty("songName")]
    public string songName; // 노래 이름

    [JsonProperty("bpm")]
    public float bpm; // 노래 BPM

    [JsonProperty("offset")]
    public float offset; // 노래 시작 오프셋

    [JsonProperty("difficulty")]
    public string difficulty; // 난이도 (Easy, Normal, Hard, Master)

    [JsonProperty("level")]
    public int level; // 난이도 레벨 (1~25)

    [JsonProperty("notes")]
    public List<NoteData> notes; // 노트 데이터 리스트
}
