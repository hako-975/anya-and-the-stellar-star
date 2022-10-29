using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineManager : MonoBehaviour
{
    public GameObject title;

    public Storyline[] storylines;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TitleFade());
    }

    IEnumerator TitleFade()
    {
        yield return new WaitForSeconds(2f);
        title.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        storylines[0].GetComponent<Animation>().Play();
    }
}
