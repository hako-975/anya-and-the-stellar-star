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
                    // if not last
                    if (currentStoryline != storylines.Length - 1)
                    {
                        storylines[currentStoryline].gameObject.SetActive(false);
                        currentStoryline += 1;

                        storylines[currentStoryline].GetComponent<CanvasGroup>().alpha = 0;
                        storylines[currentStoryline].gameObject.SetActive(true);
                        storyActive.isFinishedText = false;
                        return;
                    }
                    else
                    {
                        // if last close
                        storylines[currentStoryline].gameObject.SetActive(false);
                    }
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
}
