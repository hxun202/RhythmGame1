using UnityEngine;

[System.Serializable]
public class SongRecord
{
        public int easyScore;
        public int normalScore;
        public int hardScore;
        public int masterScore;

        public int easyCombo;
        public int normalCombo;
        public int hardCombo;
        public int masterCombo;

        public string easyRank = "-";
        public string normalRank = "-";
        public string hardRank = "-";
        public string masterRank = "-";
}
