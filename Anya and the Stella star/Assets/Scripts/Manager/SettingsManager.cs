using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    /// <IMPORTANT>
    /// Jika ada pembaharuan atau penambahan fitur pada file ini
    /// Perbaharui juga file LoadSettingsOnStart.cs
    /// </IMPORTANT>

    [HideInInspector]
    public bool isSettings = false;

    public Button backButton;

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public Slider musicSlider;
    public Slider sfxSlider;

    public TMP_Dropdown languageDropdown;

    void Update()
    {
        musicSlider.value = PlayerPrefsManager.instance.GetVolumeMusic();
        sfxSlider.value = PlayerPrefsManager.instance.GetVolumeSFX(); 
        languageDropdown.value = PlayerPrefsManager.instance.GetLanguage();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButtonAction();
        }

        backButton.onClick.AddListener(delegate { BackButtonAction(); });
    }
    public void SetVolumeMusic(float volumeMusic)
    {
        musicMixer.SetFloat("VolumeMusic", volumeMusic);
        PlayerPrefsManager.instance.SetVolumeMusic(volumeMusic);
    }

    public void SetVolumeSFX(float volumeSFX)
    {
        sfxMixer.SetFloat("VolumeSFX", volumeSFX);
        PlayerPrefsManager.instance.SetVolumeSFX(volumeSFX);
    }
    public void ResetButton()
    {
        PlayerPrefsManager.instance.DeleteKey("VolumeMusic");
        PlayerPrefsManager.instance.DeleteKey("VolumeSFX");
        PlayerPrefsManager.instance.DeleteKey("LanguageIndex");
    }

    void BackButtonAction()
    {
        isSettings = false;
        gameObject.SetActive(false);
    }
}
