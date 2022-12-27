using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : Storyline
{
    [Header("Choices")]
    public ChoiceStruct[] choice;

    public GameObject prefabChoiceButton;

    public GameObject choiceButtonList;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < choice.Length; i++)
        {
            GameObject choiceInstantiate = Instantiate(prefabChoiceButton, Vector3.zero, Quaternion.identity, choiceButtonList.transform);
            // text button
            choiceInstantiate.GetComponent<ChoiceButton>().buttonText.text = choice[i].choiceText;

            // storyline
            int j = i;
            choiceInstantiate.GetComponent<Button>().onClick.AddListener(() => {
                choiceButtonList.SetActive(false);
                choice[j].storylineChoice.SetActive(true);
            });
        }
    }
}
