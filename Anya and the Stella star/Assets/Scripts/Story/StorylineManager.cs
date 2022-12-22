using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorylineManager : MonoBehaviour
{
    public GameObject title;
    public GameObject settingsPanel;
    public GameObject historyPanel;

    public Storyline[] storylines;


    // Start is called before the first frame update
    void Start()
    {
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
