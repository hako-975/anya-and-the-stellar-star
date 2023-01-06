using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public GameObject contentHistory;
    public GameObject chatBoxPrefabs;

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
            GameObject historyChat = Instantiate(chatBoxPrefabs, Vector3.zero, Quaternion.identity, contentHistory.transform);
            historyChat.GetComponent<ChatBox>().nameText.text = PlayerPrefs.GetString("HistoryName" + i, "???");
            float R = PlayerPrefs.GetFloat("HistoryColorR" + i);
            float G = PlayerPrefs.GetFloat("HistoryColorG" + i);
            float B = PlayerPrefs.GetFloat("HistoryColorB" + i);
            historyChat.GetComponent<ChatBox>().nameText.color = new Color(R, G, B);
            historyChat.GetComponent<ChatBox>().conversationText.text = PlayerPrefs.GetString("HistoryConversation" + i);
        }
    }
}
