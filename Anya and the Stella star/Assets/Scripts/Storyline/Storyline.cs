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

    [Header("References Button")]
    public Button historyButton;
    public Button autoButton;
    public Button saveButton;
    public Button loadButton;
    public Button settingsButton;


    [HideInInspector]
    public bool isFinishedText = false;

    protected float delay;
    string currentText = "";

    protected StorylineManager storylineManager;

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

        if (transform.parent.name == "Storyline Manager")
        {
            conversationPanel.GetComponent<Button>().onClick.AddListener(() => 
            {
                storylineManager.NextStoryline();
            });
        }

        historyButton.onClick.AddListener(() =>
        {
            storylineManager.historyPanel.SetActive(true);
        });

        autoButton.onClick.AddListener(() =>
        {
            if (PlayerPrefsManager.instance.GetBoolIsAuto() == 1)
            {
                // set bool false
                PlayerPrefsManager.instance.SetBoolIsAuto(0);
            }
            else
            {
                // set bool true
                PlayerPrefsManager.instance.SetBoolIsAuto(1);
            }
        });

        saveButton.onClick.AddListener(() =>
        {
            // do something save current storyline
        });

        loadButton.onClick.AddListener(() =>
        {
            // do something load current storyline
        });

        settingsButton.onClick.AddListener(() =>
        {
            storylineManager.settingsPanel.SetActive(true);
        });
    }

    void Update()
    {
        if (PlayerPrefsManager.instance.GetBoolIsAuto() == 1)
        {
            autoButton.GetComponent<Image>().color = Color.red;
            if (isFinishedText)
            {
                // BUG!!!
                // StartCoroutine(SleepAndNext(1f));
            }
        }
        else
        {
            autoButton.GetComponent<Image>().color = new Color(0.1803922f, 0.1411765f, 0.07450981f);
        }
    }

    IEnumerator SleepAndNext(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        storylineManager.NextStoryline();
    }

    public IEnumerator ShowText()
    {
        for (int i = 0; i <= conversation.Length; i++)
        {
            if (isFinishedText)
            {
                conversationText.text = conversation;
                break; 
            }
            else
            {
                currentText = conversation.Substring(0, i);
                conversationText.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }

        PlayerPrefsManager.instance.SetHistoryName(characterName, PlayerPrefs.GetInt("HistoryCount", 0));
        PlayerPrefsManager.instance.SetHistoryColor(nameText.color, PlayerPrefs.GetInt("HistoryCount", 0));
        PlayerPrefsManager.instance.SetHistoryConversation(conversation, PlayerPrefs.GetInt("HistoryCount", 0));
        PlayerPrefsManager.instance.SetHistoryCount(PlayerPrefs.GetInt("HistoryCount", 0) + 1);

        isFinishedText = true;
    }

}
