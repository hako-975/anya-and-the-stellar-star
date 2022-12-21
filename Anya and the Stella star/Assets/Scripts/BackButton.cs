using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button backButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButtonAction();
        }

        backButton.onClick.AddListener(delegate { BackButtonAction(); });
    }

    void BackButtonAction()
    {
        gameObject.SetActive(false);
    }
}
