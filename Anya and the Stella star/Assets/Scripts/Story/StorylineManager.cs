using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineManager : MonoBehaviour
{
    public GameObject title;
    public GameObject settingsPanel;
    public GameObject historyPanel;

    public Storyline[] storylines;

    int currentStoryline = 0;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void StartTitleFade()
    {
        StartCoroutine(TitleFade());
    }

    public void NextStoryline()
    {
        foreach (Storyline storyActive in storylines)
        {
            if (storyActive.gameObject.activeSelf)
            {
                if (storyActive.isFinishedText)
                {
                    bool hasAnim = false;
                    string stateName = "";
                    foreach (AnimationState state in storylines[currentStoryline].animationStoryline)
                    {
                        if (state.name == "CharacterFadeOut" || state.name == "StorylineFadeOut")
                        {
                            stateName = state.name;
                            hasAnim = true;
                            break;
                        } 
                        else
                        {
                            hasAnim = false;
                        }
                    }

                    StartCoroutine(HasAnimation(hasAnim, stateName));
                    storyActive.isFinishedText = false;
                    return;
                }
                else
                {
                    storyActive.isFinishedText = true;
                    return;
                }
            }

        }
    }

    IEnumerator TitleFade()
    {
        title.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        storylines[0].gameObject.SetActive(true);
    }

    IEnumerator HasAnimation(bool has, string stateName)
    {
        if (has)
        {
            storylines[currentStoryline].animationStoryline.Play(stateName);
            yield return new WaitForSeconds(1);
            storylines[currentStoryline].gameObject.SetActive(false);
        }
        else
        {
            storylines[currentStoryline].gameObject.SetActive(false);
        }

        if (currentStoryline != storylines.Length - 1)
        {
            currentStoryline += 1;
            storylines[currentStoryline].gameObject.SetActive(true);
        }

        yield return null;
    }
}