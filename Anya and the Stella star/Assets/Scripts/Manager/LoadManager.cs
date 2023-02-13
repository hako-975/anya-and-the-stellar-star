using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public Button[] btnSaveGame;

    void OnEnable()
    {
        UpdateDataUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < btnSaveGame.Length; i++)
        {
            int j = i;
            btnSaveGame[j].onClick.AddListener(() =>
            {
                string gameData = PlayerPrefsManager.instance.GetSave(j);
                if (gameData != "")
                {
                    string[] splitGameData = gameData.Split('|');
                    SceneManager.LoadScene(splitGameData[0]);
                }
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
