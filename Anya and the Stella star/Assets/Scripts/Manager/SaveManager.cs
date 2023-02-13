using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public Button[] btnSaveGame;

    public StorylineManager storylineManager;
    
    int pages = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateDataUI();

        string title = storylineManager.title.GetComponentInChildren<TextMeshProUGUI>().text;
        string storyTo = SceneManager.GetActiveScene().name;
        
        for (int i = 0; i < btnSaveGame.Length; i++)
        {
            int j = i;
            btnSaveGame[j].onClick.AddListener(() =>
            {
                pages = PlayerPrefs.GetInt("HistoryCount", 0);

                PlayerPrefsManager.instance.SetSave(j, storyTo, pages, title);
                UpdateDataUI();
            });
        }
    }

    void UpdateDataUI()
    {
        for (int i = 0; i < btnSaveGame.Length; i++)
        {
            var btnGameData = btnSaveGame[i].GetComponent<GameData>();
            string gameData = PlayerPrefsManager.instance.GetSave(i);
            if (gameData != "")
            {
                string[] splitGameData = gameData.Split('|');

                btnGameData.pages.text = "Pages " + splitGameData[1];
                btnGameData.title.text = splitGameData[2];
                btnGameData.dateTimeSaved.text = splitGameData[3];
            }
            else
            {
                btnGameData.pages.text = "";
                btnGameData.title.text = "Empty";
                btnGameData.dateTimeSaved.text = "";
            }
        }
        
    }
}
