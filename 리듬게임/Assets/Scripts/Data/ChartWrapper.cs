using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ChartWrapper
{
    public string songName; // 노래 이름
    public float bpm; // 노래 BPM
    public float offset; // 노래 시작 오프셋
    public string difficulty; // 난이도 (Easy, Normal, Hard, Master)

    public List<NoteData> notes; // 노트 데이터 리스트
}
