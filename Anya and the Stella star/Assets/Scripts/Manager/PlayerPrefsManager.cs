using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    #region Singleton
    public static PlayerPrefsManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public string GetNextScene()
    {
        return PlayerPrefs.GetString("NextScene", "Main Menu");
    }

    public void SetNextScene(string nextScene)
    {
        PlayerPrefs.SetString("NextScene", nextScene);
        SceneManager.LoadScene("Loading");
    }

    #region Level
    public int GetLevelAt()
    {
        return PlayerPrefs.GetInt("LevelAt", 1);
    }

    public int SetLevelAt(int level)
    {
        PlayerPrefs.SetInt("LevelAt", level);
        return GetLevelAt();
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    public int SetCurrentLevel()
    {
        // level 1, build index 1
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
        return GetCurrentLevel();
    }
    #endregion

    #region Settings
    public float GetVolumeMusic()
    {
        return PlayerPrefs.GetFloat("VolumeMusic", -5f);
    }

    public float SetVolumeMusic(float volumeMusic)
    {
        PlayerPrefs.SetFloat("VolumeMusic", volumeMusic);
        return GetVolumeMusic();
    }

    public float GetVolumeSFX()
    {
        return PlayerPrefs.GetFloat("VolumeSFX", -5f);
    }

    public float SetVolumeSFX(float volumeSFX)
    {
        PlayerPrefs.SetFloat("VolumeSFX", volumeSFX);
        return GetVolumeSFX();
    }

    public float GetTextSpeed()
    {
        return PlayerPrefs.GetFloat("TextSpeed", 0.1f);
    }

    public float SetTextSpeed(float textSpeed)
    {
        PlayerPrefs.SetFloat("TextSpeed", textSpeed);
        return GetTextSpeed();
    }

    public int GetLanguage()
    {
        // 2 is index for english
        return PlayerPrefs.GetInt("LanguageIndex", 2);
    }

    public int SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt("LanguageIndex", languageIndex);
        return GetLanguage();
    }
    #endregion

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}
