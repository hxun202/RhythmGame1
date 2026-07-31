using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class NoteData
{
    [JsonProperty("time")]
    public float time; // 노트가 떨어지는 시간

    [JsonProperty("endTime")]
    public float endTime; // 홀드 노트의 끝나는 시간 (홀드 노트가 아닌 경우 0)

    [JsonProperty("lane")]
    public int lane; // 노트가 떨어지는 위치 (0~5)

    [JsonProperty("targetLane")]
    public int targetLane; // 홀드 노트의 목표 위치 (홀드 노트가 아닌 경우 -1)

    [JsonProperty("type")]
    public string type; // 노트의 타입 (tap, hold, etc.)

    [JsonProperty("direction")]
    public string direction; // 노트의 방향 (up)
}

public enum MusicDifficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2,
    Master = 3
}

public enum NoteType
{
    Tap = 0,
    Hold = 1,
    Slide = 2,
    Flick = 3
}