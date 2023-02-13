using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public GameObject contentHistory;
    public GameObject dialogBoxHistoryPrefab;

    void Start()
    {
        PlayerPrefsManager.instance.ResetHistory();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        foreach (Transform child in contentHistory.transform) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("HistoryCount", 0); i++)
        {
            GameObject historyChat = Instantiate(dialogBoxHistoryPrefab, Vector3.zero, Quaternion.identity, contentHistory.transform);
            
            var conversationText = historyChat.GetComponent<DialogBoxHistory>().conversationText;
            conversationText.text = PlayerPrefs.GetString("HistoryConversation" + i);

            float newTop = 25f;
            float oldTop = 110f;
            var recConversation = conversationText.GetComponent<RectTransform>();

            var nameText = historyChat.GetComponent<DialogBoxHistory>().nameText;
            nameText.text = PlayerPrefs.GetString("HistoryName" + i, "???");
            
            if (nameText.text != "")
            {
                float R = PlayerPrefs.GetFloat("HistoryColorR" + i);
                float G = PlayerPrefs.GetFloat("HistoryColorG" + i);
                float B = PlayerPrefs.GetFloat("HistoryColorB" + i);
                nameText.color = new Color(R, G, B);
                
                // set conversation height
                recConversation.offsetMax = new Vector2(recConversation.offsetMax.x, -oldTop);
            }
            else
            {
                // set conversation height
                recConversation.offsetMax = new Vector2(recConversation.offsetMax.x, -newTop);
            }
        }
    }
}
