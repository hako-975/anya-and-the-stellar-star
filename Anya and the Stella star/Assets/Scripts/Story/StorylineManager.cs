using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineManager : MonoBehaviour
{
    int currentStoryline;

    public GameObject title;

    public Storyline[] storylines;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject closeButton;

    // Start is called before the first frame update
    void Start()
    {
        ResetPages();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentStoryline != 0)
            {
                PreviousButton();
            }
            else
            {
                CloseButton();
            }
        }
    }

    public void NextButton()
    {
        // if not end of storylines
        if (currentStoryline != storylines.Length - 1)
        {
            storylines[currentStoryline].gameObject.SetActive(false);

            currentStoryline += 1;
            storylines[currentStoryline].gameObject.SetActive(true);
        }

        SetActiveButton();
    }

    public void PreviousButton()
    {
        // if not first of storylines
        if (currentStoryline != 0)
        {
            storylines[currentStoryline].gameObject.SetActive(false);

            currentStoryline -= 1;
            storylines[currentStoryline].gameObject.SetActive(true);
        }

        SetActiveButton();
    }

    void SetActiveButton()
    {
        // if current page == first
        if (currentStoryline == 0)
        {
            // disable prev
            previousButton.SetActive(false);

            // if current page not last
            if (currentStoryline != storylines.Length - 1)
            {
                nextButton.SetActive(true);
                closeButton.SetActive(false);
            }
            else
            {
                closeButton.SetActive(true);
                nextButton.SetActive(false);
            }
        }
        // if current page == last
        else if (currentStoryline == storylines.Length - 1)
        {
            closeButton.SetActive(true);

            // disable next
            nextButton.SetActive(false);

            // if current page not first
            if (currentStoryline != 0)
            {
                previousButton.SetActive(true);
            }
            else
            {
                previousButton.SetActive(false);
            }
        }
        else
        {
            if (currentStoryline == storylines.Length - 1)
            {
                closeButton.SetActive(true);
            }
            else
            {
                closeButton.SetActive(false);
            }

            nextButton.SetActive(true);
            previousButton.SetActive(true);
        }
    }

    void ResetPages()
    {
        closeButton.SetActive(false);

        // set all deactive
        for (int i = 0; i < storylines.Length; i++)
        {
            storylines[i].gameObject.SetActive(false);
        }

        currentStoryline = 0;

        if (currentStoryline == storylines.Length - 1)
        {
            closeButton.SetActive(true);
        }

        // set first active
        // storylines[currentStoryline].gameObject.SetActive(true);

        SetActiveButton();
    }

    public void CloseButton()
    {
        ResetPages();
        gameObject.SetActive(false);
    }

    public void StartTitleFade()
    {
        StartCoroutine(TitleFade());
    }

    IEnumerator TitleFade()
    {
        title.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        storylines[0].gameObject.SetActive(true);
    }
}
