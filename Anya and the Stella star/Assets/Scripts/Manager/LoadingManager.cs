using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Image loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        loadingBar.fillAmount = 0;

        StartCoroutine(LoadAsync(PlayerPrefsManager.instance.GetNextScene()));
    }

    IEnumerator LoadAsync(string nextScene)
    {
        PlayerPrefsManager.instance.DeleteKey("NextScene");

        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);

        if (async == null)
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            while (!async.isDone)
            {
                float progress = Mathf.Clamp01(async.progress);
                loadingBar.fillAmount = progress;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
