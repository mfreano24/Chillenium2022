using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyTestScript : MonoBehaviour
{

    [TextArea(5, 5)]
    public string[] lines;

    public Sprite[] icons;

    public AudioClip clip;


    


    private void Start()
    {
        DialogueBubble.Instance.LoadDialogue(lines, icons, clip);

        DialogueBubble.Instance.PlayDialogue();
    }

}
