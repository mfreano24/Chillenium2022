using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    static DialogueBubble m_instance;

    public static DialogueBubble Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        if(m_instance != null)
        {
            Debug.LogError("Already a Dialoguebubble in this scene!!");
        }

        m_instance = this;
    }


    Sprite[] loadedIcons;
    string[] loadedDialogue;
    

    public Image dialogueImage;
    public Text dialogueText;
    AudioSource aud;

    Animator anim;

    public GameObject DialogueBoxParent;

    
    bool textFinished = false;
    bool moveToNextBox = false;
    bool skipToEnd = false;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textFinished)
            {
                moveToNextBox = true;
            }
            else
            {
                skipToEnd = true;
            }
        }
    }


    public void PlayDialogue()
    {
        DialogueBoxParent.SetActive(true);
        anim.Play("DialogueBoxEnter");

        StartCoroutine(LoopDialogue());
    }

    IEnumerator LoopDialogue()
    {
        for(int i = 0; i < loadedDialogue.Length; i++)
        {
            //set icon here
            yield return DrawText(loadedDialogue[i]);
        }

        ExitDialogue();
    }

    IEnumerator DrawText(string dlog)
    {
        dialogueText.text = "";
        string curr = "";
        for(int i = 0; i < dlog.Length; i++)
        {
            curr += dlog[i];

            dialogueText.text = curr;

            if (skipToEnd)
            {
                curr = dlog;
                dialogueText.text = curr;
                break;
            }

            //aud.Play();

            yield return new WaitForSeconds(0.05f);
        }

        skipToEnd = false;
        textFinished = true;

        yield return new WaitUntil(() => moveToNextBox);
        textFinished = false;
        moveToNextBox = false;

    }


    public void ExitDialogue()
    {
        anim.Play("DialogueBoxExit");

        StartCoroutine(WaitToDisable());
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(1.0f);
        DialogueBoxParent.SetActive(false);
    }




    public void LoadDialogue(string[] dialogue, Sprite[] icons, AudioClip textSound)
    {
        loadedDialogue = dialogue;
        loadedIcons = icons;
        aud.clip = textSound;
    }
}
