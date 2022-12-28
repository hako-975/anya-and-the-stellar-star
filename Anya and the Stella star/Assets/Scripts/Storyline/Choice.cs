using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : Storyline
{
    [Header("Choice Data")]
    public ChoiceData[] choiceData;

    [Header("References Object")]
    public GameObject prefabChoiceButton;
    public GameObject choiceButtonList;

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


        for (int i = 0; i < choiceData.Length; i++)
        {
            GameObject choiceInstantiate = Instantiate(prefabChoiceButton, Vector3.zero, Quaternion.identity, choiceButtonList.transform);
            // text button
            choiceInstantiate.GetComponent<ChoiceButton>().buttonText.text = choiceData[i].choiceText;

            // storyline
            int j = i;
            choiceInstantiate.GetComponent<Button>().onClick.AddListener(() => 
            {
                // remove choice button list
                choiceButtonList.SetActive(false);

                // set first storyline choice active
                choiceData[j].storylineChoice[0].SetActive(true);


                for (int k = 0; k < choiceData[j].storylineChoice.Length; k++)
                {
                    int l = k;

                    if (l != choiceData[j].storylineChoice.Length - 1)
                    {
                        choiceData[j].storylineChoice[l].GetComponent<Storyline>().conversationPanel.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            if (choiceData[j].storylineChoice[l].GetComponent<Storyline>().isFinishedText)
                            {
                                choiceData[j].storylineChoice[l].SetActive(false);
                                choiceData[j].storylineChoice[l + 1].SetActive(true);
                                return;
                            }
                            else
                            {
                                choiceData[j].storylineChoice[l].GetComponent<Storyline>().isFinishedText = true;
                                return;
                            }
                        });
                    }
                    else
                    {
                        choiceData[j].storylineChoice[l].GetComponent<Storyline>().conversationPanel.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            if (choiceData[j].storylineChoice[l].GetComponent<Storyline>().isFinishedText)
                            {
                                choiceData[j].storylineChoice[l].SetActive(false);
                                storylineManager.NextStoryline();
                                return;
                            }
                            else
                            {
                                choiceData[j].storylineChoice[l].GetComponent<Storyline>().isFinishedText = true;
                                return;
                            }
                        });
                    }

                }
            });
        }
    }
}
