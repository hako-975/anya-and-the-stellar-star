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

            var rec = createStory.GetComponent<RectTransform>();
            rec.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthStory);
            rec.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightStory);

            var storyRef = createStory.GetComponent<StoryReference>();
            storyRef.storyTo.text = "Story " + (i + 1).ToString();
            storyRef.titleStory.text = stories[i].GetComponent<Story>().titleStory;
            storyRef.coverStory.sprite = stories[i].GetComponent<Story>().coverStory;

            var storyBtn = createStory.GetComponent<Button>();

            if (i < levelAt)
            {
                storyBtn.interactable = true;
            }
            else
            {
                storyBtn.interactable = false;
            }

            int j = i+1;
            storyBtn.onClick.AddListener(() =>
            {
                PlayerPrefsManager.instance.SetNextScene("Story " + j);
            });
        }
    }

    public void BackButtonAction()
    {
        PlayerPrefsManager.instance.SetNextScene("Main Menu");
    }
}
