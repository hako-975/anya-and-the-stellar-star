using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStoryManager : MonoBehaviour
{
    public GameObject content;
    public GameObject storyPrefab;

    Transform[] stories;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefsManager.instance.GetLevelAt();
        
        var storyList = new List<Transform>();

        // get child in transform (current transform)
        foreach (Transform child in transform)
        {
            storyList.Add(child);
        }

        stories = storyList.ToArray();
        
        if (levelAt > stories.Length)
        {
            levelAt = stories.Length;
        }

        for (int i = 0; i < stories.Length; i++)
        {
            GameObject createStory = Instantiate(storyPrefab, content.transform);
            
            float widthStory = 1296f;
            float heightStory = 2304f;
            createStory.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthStory);
            createStory.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightStory);
         
            createStory.GetComponent<StoryReference>().storyTo.text = "Story " + (i + 1).ToString();
            createStory.GetComponent<StoryReference>().titleStory.text = stories[i].GetComponent<Story>().titleStory;
            createStory.GetComponent<StoryReference>().coverStory.sprite = stories[i].GetComponent<Story>().coverStory;

            if (i < levelAt)
            {
                createStory.GetComponent<Button>().interactable = true;
            }
            else
            {
                createStory.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void BackButtonAction()
    {
        PlayerPrefsManager.instance.SetNextScene("Main Menu");
    }
}
