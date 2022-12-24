using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Storyline))]
public class Storyline_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Storyline script = (Storyline)target;

        script.hasCharacter = EditorGUILayout.Toggle("Has Character", script.hasCharacter);

        if (script.hasCharacter)
        {
            EditorUtility.SetDirty(target);

            script.character = (Characters)EditorGUILayout.ObjectField("Character", script.character, typeof(Characters), true);

            script.mood = (Storyline.Mood)EditorGUILayout.EnumPopup("Mood", script.mood);

            script.characterImage = (Image)EditorGUILayout.ObjectField("Character Image", script.characterImage, typeof(Image), true);
            
            script.nameText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Character Name", script.nameText, typeof(TextMeshProUGUI), true);
        }
    }
}
