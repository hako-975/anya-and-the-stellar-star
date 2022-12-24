using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        loadingBar.value = 0;

        StartCoroutine(LoadAsync(PlayerPrefsManager.instance.GetNextScene()));
    }

    IEnumerator LoadAsync(string nextScene)
    {
        PlayerPrefsManager.instance.DeleteKey("NextScene");

        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        
        async.allowSceneActivation = false;

        if (async == null)
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            while (!async.isDone)
            {
                float progress = Mathf.Clamp01(async.progress);
                loadingBar.value = progress;
                
                if (async.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(1);
                    async.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        
    }
}
