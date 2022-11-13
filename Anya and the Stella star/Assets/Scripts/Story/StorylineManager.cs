using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        for (int i = 0; i < storylines.Length; i++)
        {
            if ((i == 0) && (i != storylines.Length - 1))
            {
                var gameObjectNextButton = Instantiate(nextButton, transform.position, Quaternion.identity, storylines[i].gameObject.transform);
                gameObjectNextButton.transform.localPosition = new Vector3(450f, -750f);
                gameObjectNextButton.GetComponent<Button>().onClick.AddListener(NextButton);
            }
            else if ((i == storylines.Length - 1) && (i != 0))
            {
                var gameObjectPreviousButton = Instantiate(previousButton, transform.position, Quaternion.identity, storylines[i].gameObject.transform);
                gameObjectPreviousButton.transform.localPosition = new Vector3(-175f, -750f);
                gameObjectPreviousButton.GetComponent<Button>().onClick.AddListener(PreviousButton);

                var gameObjectCloseButton = Instantiate(closeButton, transform.position, Quaternion.identity, storylines[storylines.Length - 1].gameObject.transform);
                gameObjectCloseButton.transform.localPosition = new Vector3(450f, -750f);
                gameObjectCloseButton.GetComponent<Button>().onClick.AddListener(CloseButton);
            }
            else if (i == storylines.Length - 1)
            {
                var gameObjectCloseButton = Instantiate(closeButton, transform.position, Quaternion.identity, storylines[storylines.Length - 1].gameObject.transform);
                gameObjectCloseButton.transform.localPosition = new Vector3(450f, -750f);
                gameObjectCloseButton.GetComponent<Button>().onClick.AddListener(CloseButton);
            }
            else
            {
                var gameObjectPreviousButton = Instantiate(previousButton, transform.position, Quaternion.identity, storylines[i].gameObject.transform);
                gameObjectPreviousButton.transform.localPosition = new Vector3(-175f, -750f);
                gameObjectPreviousButton.GetComponent<Button>().onClick.AddListener(PreviousButton);

                var gameObjectNextButton = Instantiate(nextButton, transform.position, Quaternion.identity, storylines[i].gameObject.transform);
                gameObjectNextButton.transform.localPosition = new Vector3(450f, -750f);
                gameObjectNextButton.GetComponent<Button>().onClick.AddListener(NextButton);
            }
        }
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
            storylines[currentStoryline].GetComponent<CanvasGroup>().alpha = 0;
            storylines[currentStoryline].gameObject.SetActive(true);
            storylines[currentStoryline].GetComponent<Animation>().Play();
        }
    }

    public void PreviousButton()
    {
        // if not first of storylines
        if (currentStoryline != 0)
        {
            storylines[currentStoryline].gameObject.SetActive(false);

            currentStoryline -= 1;
            storylines[currentStoryline].GetComponent<CanvasGroup>().alpha = 0;
            storylines[currentStoryline].gameObject.SetActive(true);
            storylines[currentStoryline].GetComponent<Animation>().Play();
        }
    }


    public void CloseButton()
    {
        storylines[currentStoryline].GetComponent<Animation>().PlayQueued("StorylineSlideRight");
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
