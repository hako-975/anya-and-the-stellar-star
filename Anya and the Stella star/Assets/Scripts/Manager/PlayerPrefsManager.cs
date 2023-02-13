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

    #region Scene
    public string GetNextScene()
    {
        return PlayerPrefs.GetString("NextScene", "Main Menu");
    }

    public void SetNextScene(string nextScene)
    {
        PlayerPrefs.SetString("NextScene", nextScene);
        SceneManager.LoadScene("Loading");
    }
    #endregion

    #region Auto
    public int GetBoolIsAuto()
    {
        return PlayerPrefs.GetInt("IsAuto", 0);
    }

    public int SetBoolIsAuto(int boolean)
    {
        PlayerPrefs.SetInt("IsAuto", boolean);
        return GetBoolIsAuto();
    }
    #endregion

    #region History
    public void SetHistoryCount(int count)
    {
        PlayerPrefs.SetInt("HistoryCount", count);
    }

    public void SetHistoryName(string name, int historyTo)
    {
        PlayerPrefs.SetString("HistoryName" + historyTo, name);
    }

    public void SetHistoryColor(Color color, int historyTo)
    {
        // ex: RGBA(1.000, 1.000, 1.000, 1.000)
        PlayerPrefs.SetFloat("HistoryColorR" + historyTo, color.r);
        PlayerPrefs.SetFloat("HistoryColorG" + historyTo, color.g);
        PlayerPrefs.SetFloat("HistoryColorB" + historyTo, color.b);
    }

    public void SetHistoryConversation(string conversation, int historyTo)
    {
        PlayerPrefs.SetString("HistoryConversation" + historyTo, conversation);
    }

    public void ResetHistory()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("HistoryCount", 0); i++)
        {
            DeleteKey("HistoryName" + i);
            DeleteKey("HistoryColorR" + i);
            DeleteKey("HistoryColorG" + i);
            DeleteKey("HistoryColorB" + i);
            DeleteKey("HistoryConversation" + i);
        }

        DeleteKey("HistoryCount");
    }

    #endregion

    #region Save Load Data
    public void SetSave(int saveDataTo, string storyTo, int pages, string title)
    {
        PlayerPrefs.SetString("SaveData" + saveDataTo, storyTo +"|"+ pages + "|" + title + "|" + System.DateTime.Now.ToString("HH:mm tt - dd/MM/yyyy"));
        PlayerPrefs.Save();
    }

    public string GetSave(int saveDataTo)
    {
        return PlayerPrefs.GetString("SaveData" + saveDataTo);
    }
    #endregion

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
