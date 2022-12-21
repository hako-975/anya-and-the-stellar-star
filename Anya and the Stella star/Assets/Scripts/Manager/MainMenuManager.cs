using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject dialogQuitPanel;

    public void PlayButton()
    {
        if (PlayerPrefsManager.instance.GetLevelAt() == 1)
        {
           PlayerPrefsManager.instance.SetNextScene("Story 1");
        }
        else
        {
            PlayerPrefsManager.instance.SetNextScene("Story " + PlayerPrefsManager.instance.GetLevelAt());
        }
    }

    public void SelectStoryButton()
    {
        PlayerPrefsManager.instance.SetNextScene("Select Story");
    }

    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void CreditsPanel()
    {
        creditsPanel.SetActive(true);
    }

    public void DialogQuitPanel()
    {
        dialogQuitPanel.SetActive(true);
    }

}
