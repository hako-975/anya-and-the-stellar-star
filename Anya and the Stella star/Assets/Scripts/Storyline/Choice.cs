using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : Storyline
{
    [Header("Choices")]
    public ChoiceStruct[] choice;

    public GameObject prefabChoiceButton;

    public GameObject choiceButtonList;


    // Start is called before the first frame update
    void Start()
    {

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


        for (int i = 0; i < choice.Length; i++)
        {
            GameObject choiceInstantiate = Instantiate(prefabChoiceButton, Vector3.zero, Quaternion.identity, choiceButtonList.transform);
            // text button
            choiceInstantiate.GetComponent<ChoiceButton>().buttonText.text = choice[i].choiceText;

            // storyline
            int j = i;
            choiceInstantiate.GetComponent<Button>().onClick.AddListener(() => {
                choiceButtonList.SetActive(false);
                choice[j].storylineChoice.SetActive(true);
            });
        }
    }
}
