using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storyline : MonoBehaviour
{
    public Characters character;
    
    public enum Mood
    {
        amazed,
        happy,
        hello,
        idle
    }

    public Mood mood;

    [TextArea]
    public string conversation;

    private int moodIndex = 0;

    [Header("References Object")]
    public Image characterImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI conversationText;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < character.charactersSprite.Length; i++)
        {
            if (mood.ToString() == character.charactersSprite[i].name)
            {
                moodIndex = i;
                break;
            }
        }

        characterImage.sprite = character.charactersSprite[moodIndex];
        nameText.text = character.name;
        conversationText.text = conversation;
    }
}
