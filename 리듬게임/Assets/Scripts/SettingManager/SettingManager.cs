using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider noteSlider;

    public GameObject settingPanel;

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1f);
        noteSlider.value = PlayerPrefs.GetFloat("Note", 1f);

        ApplyVolume();
    }

    public void ApplyVolume()
    {
        mixer.SetFloat("Master", Mathf.Log10(Mathf.Max(masterSlider.value, 0.0001f)) * 20);
        mixer.SetFloat("Music", Mathf.Log10(Mathf.Max(musicSlider.value, 0.0001f)) * 20);
        mixer.SetFloat("Note", Mathf.Log10(Mathf.Max(noteSlider.value, 0.0001f)) * 20);
    }
    public void ApplySetting()
    {
        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("Note", noteSlider.value);

        PlayerPrefs.Save();

        ApplyVolume();
    }

    public void ResetSetting()
    {
        masterSlider.value = 1f;
        musicSlider.value = 1f;
        noteSlider.value = 1f;

        ApplySetting();
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
}
