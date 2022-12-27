using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storyline : MonoBehaviour
{
    [Header("Character")]
    public string characterName = "???";
    
    public Color characterColorName = Color.white;
    
    public Sprite characterMoodLeft;
    public Sprite characterMoodMiddle;
    public Sprite characterMoodRight;

    public Sprite backgroundImage;

    public AudioClip backgroundMusic;
    public AudioClip voiceCharacter;

    public enum ConversationPanelType
    {
        withName,
        withoutName
    }

    public ConversationPanelType conversationPanelType;

    [TextArea(7, 7)]
    public string conversation;

    [Header("References Object")]
    public Image characterImageLeft;
    public Image characterImageMiddle;
    public Image characterImageRight;

    public GameObject conversationPanel;

    public Sprite conversationPanelWithName;
    public Sprite conversationPanelWithoutName;

    public Image backgroundImageReferences;

    public AudioSource backgroundMusicReferences;
    public AudioSource voiceCharacterReferences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI conversationText;

    [HideInInspector]
    public bool isFinishedText = false;

    public float delay;
    string currentText = "";

    StorylineManager storylineManager;

    // Start is called before the first frame update
    void Start()
    {
        storylineManager = GetComponentInParent<StorylineManager>();

        if (backgroundImage == null)
        {
            backgroundImageReferences.sprite = null;
            backgroundImageReferences.color = Color.clear;
        }
        else
        {
            backgroundImageReferences.sprite = backgroundImage;
        }

        nameText.text = characterName;
        nameText.color = characterColorName;
        
        delay = PlayerPrefsManager.instance.GetTextSpeed();
        conversationText.text = "";
        StartCoroutine(ShowText());

        if (characterMoodLeft != null)
        {
            characterImageLeft.sprite = characterMoodLeft;
            characterImageLeft.color = Color.white;
        }

        if (characterMoodMiddle != null)
        {
            characterImageMiddle.sprite = characterMoodMiddle;
            characterImageMiddle.color = Color.white;
        }

        if (characterMoodRight != null)
        {
            characterImageRight.sprite = characterMoodRight;
            characterImageRight.color = Color.white;
        }

        if (conversationPanelType.ToString() == "withName")
        {
            nameText.enabled = true;
            conversationPanel.GetComponent<Image>().sprite = conversationPanelWithName;
        } 
        else if (conversationPanelType.ToString() == "withoutName")
        {
            nameText.enabled = false;
            conversationPanel.GetComponent<Image>().sprite = conversationPanelWithoutName;
        }

        if (backgroundMusic)
        {
            backgroundMusicReferences.Stop();
            backgroundMusicReferences.clip = backgroundMusic;
            backgroundMusicReferences.playOnAwake = true;
            backgroundMusicReferences.loop = true;
            backgroundMusicReferences.Play();
        }

        if (voiceCharacter)
        {
            voiceCharacterReferences.Stop();
            voiceCharacterReferences.clip = voiceCharacter;
            backgroundMusicReferences.playOnAwake = true;
            backgroundMusicReferences.loop = false;
            voiceCharacterReferences.Play();
        }

        conversationPanel.GetComponent<Button>().onClick.AddListener(NextStoryline);
    }

    void Update()
    {
        if (conversationText.text == "")
        {
            StartCoroutine(ShowText());
        }
    }

    void NextStoryline()
    {
        storylineManager.NextStoryline();
    }

    public IEnumerator ShowText()
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
