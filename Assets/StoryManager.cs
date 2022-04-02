using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    static StoryManager m_instance;

    public static StoryManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogError("Already a StoryManager in this scene!!");
        }

        m_instance = this;
    }


    public List<DialogueScene> dialogueScenes; //0 and 1 will be intro, 2 - ??? will be random pairs of dialogue before and after a battle
    public List<Sprite> spriteAtlas;
    public List<AudioClip> sfxAtlas;

    private void Start()
    {
        PlayScene(0);
    }


    public void PlayScene(int sceneIndex)
    {
        List<Sprite> sceneSprites = new List<Sprite>();
        List<AudioClip> clips = new List<AudioClip>();

        DialogueBubble.Instance.LoadDialogue(dialogueScenes[sceneIndex].lines);
        DialogueBubble.Instance.PlayDialogue();
    }
}
