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



    List<Dialogue> dloglines; //each contains a line, sound effect id, and talksprite id.
    

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

        DialogueBoxParent.SetActive(false);
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
        if(dloglines == null)
        {
            Debug.LogError("dloglines is null!");
        }

        for(int i = 0; i < dloglines.Count; i++)
        {
            //set icon here
            yield return DrawText(dloglines[i]);
        }

        ExitDialogue();
    }

    IEnumerator DrawText(Dialogue dlog)
    {
        string line = dlog.line;
        Sprite icon = StoryManager.Instance.spriteAtlas[dlog.iconID];
        AudioClip clip = StoryManager.Instance.sfxAtlas[dlog.sfxID];

        dialogueImage.sprite = icon;
        aud.clip = clip;


        dialogueText.text = "";
        string curr = "";
        for(int i = 0; i < line.Length; i++)
        {
            curr += line[i];

            dialogueText.text = curr;

            if (skipToEnd)
            {
                curr = line;
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

    public void LoadDialogue(List<Dialogue> lines)
    {
        dloglines = lines;
    }
}
