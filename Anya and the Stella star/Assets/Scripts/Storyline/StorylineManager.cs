using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorylineManager : MonoBehaviour
{
    Transform[] storylines;

    public GameObject title;
    public GameObject settingsPanel;
    public GameObject historyPanel;

    int currentStoryline = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        var storylineList = new List<Transform>();
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            storylineList.Add(child);
        }
        
        storylines = storylineList.ToArray();
        
        title.GetComponent<Button>().onClick.AddListener(StartTitleFade);
    }

    public void StartTitleFade()
    {
        StartCoroutine(TitleFade());
    }

    public void NextStoryline()
    {
        foreach (Transform storyActive in storylines)
        {
            if (storyActive.gameObject.activeSelf)
            {
                if (storyActive.GetComponent<Storyline>().isFinishedText)
                {
                    // if not last
                    if (currentStoryline != storylines.Length - 1)
                    {
                        storylines[currentStoryline].gameObject.SetActive(false);
                        currentStoryline += 1;

                        storylines[currentStoryline].gameObject.SetActive(true);
                        storyActive.GetComponent<Storyline>().isFinishedText = false;
                        return;
                    }
                    else
                    {
                        // if last, close
                        storylines[currentStoryline].gameObject.SetActive(false);
                    }
                }
                else
                {
                    storyActive.GetComponent<Storyline>().isFinishedText = true;
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
