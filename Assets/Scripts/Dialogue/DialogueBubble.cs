using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    Sprite[] loadedIcons;
    string[] loadedDialogue;

    public Image dialogueImage;
    public Text dialogueText;

    public GameObject DialogueBoxParent;


    public void PlayDialogue()
    {
        DialogueBoxParent.SetActive(true);
    }


    public void ExitDialogue()
    {
        DialogueBoxParent.SetActive(false);
    }




    public void LoadDialogue(string[] dialogue, Sprite[] icons)
    {
        loadedDialogue = dialogue;
        loadedIcons = icons;
    }
}
