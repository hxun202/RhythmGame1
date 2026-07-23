using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button masterButton;

    public Text easyLevelText;
    public Text normalLevelText;
    public Text hardLevelText;
    public Text masterLevelText;

    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text rankText;

    public void SelectDifficulty(int difficulty)
    {
        SongManager.instance.selectedDifficulty = (Difficulty)difficulty;

        RefreshUI();
    }

    public void RefreshUI()
    {
        SongData song = SongManager.instance.selectedSong;

        SongRecord record = SongManager.instance.GetRecord(song);

        switch (SongManager.instance.selectedDifficulty)
        {
            case Difficulty.Easy:
                scoreText.text = record.easyScore.ToString();
                comboText.text = record.easyCombo.ToString();
                rankText.text = record.easyRank;
                break;

            case Difficulty.Normal:
                scoreText.text = record.normalScore.ToString();
                comboText.text = record.normalCombo.ToString();
                rankText.text = record.normalRank;
                break;

            case Difficulty.Hard:
                scoreText.text = record.hardScore.ToString();
                comboText.text = record.hardCombo.ToString();
                rankText.text = record.hardRank;
                break;

            case Difficulty.Master:
                scoreText.text = record.masterScore.ToString();
                comboText.text = record.masterCombo.ToString();
                rankText.text = record.masterRank;
                break;
        }
    }

    void Start()
    {
        SongData song = SongManager.instance.selectedSong;

        easyLevelText.text = song.easyLevel.ToString();
        normalLevelText.text = song.normalLevel.ToString();
        hardLevelText.text = song.hardLevel.ToString();
        masterLevelText.text = song.masterLevel.ToString();

        RefreshUI();
    }
}
