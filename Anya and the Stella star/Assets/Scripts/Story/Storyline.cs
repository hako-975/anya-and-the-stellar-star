using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storyline : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Animation animationStoryline;
    StorylineManager storylineManager;

    [HideInInspector]
    public bool hasCharacter;

    [HideInInspector]
    public Characters character;

    public enum Mood
    {
        amazed,
        happy,
        hello,
        idle
    }
    
    [HideInInspector]
    public Mood mood;

    [TextArea(7, 7)]
    public string conversation;

    private int moodIndex = 0;

    [Header("References Object")]
    [HideInInspector]
    public Image characterImage;
    
    [HideInInspector]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI conversationText;

    [HideInInspector]
    public bool isFinishedText = false;


    float delay;
    string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        delay = PlayerPrefsManager.instance.GetTextSpeed();

        conversationText.text = "";

        storylineManager = GetComponentInParent<StorylineManager>();
        animationStoryline = GetComponent<Animation>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        if (character != null)
        {
            for (int i = 0; i < character.charactersSprite.Length; i++)
            {
                if (mood.ToString() == character.charactersSprite[i].name)
                {
                    moodIndex = i;
                    break;
                }
            }

            characterImage.sprite = character.charactersSprite[moodIndex];
            nameText.text = character.name;
        } 
        
        animationStoryline.Play();

        StartCoroutine(ShowText());
    }

    public void HistoryButton()
    {
        storylineManager.historyPanel.SetActive(true);
    }

    public void AutoButton()
    {

    }

    public void SaveButton()
    {

    }

    public void LoadButton()
    {

    }

    public void SettingsButton()
    {
        storylineManager.settingsPanel.SetActive(true);
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= conversation.Length; i++)
        {
            if (isFinishedText)
            {
                conversationText.text = conversation;
                yield return null;
            }
            else
            {
                currentText = conversation.Substring(0, i);
                conversationText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }

        isFinishedText = true;
    }
}
