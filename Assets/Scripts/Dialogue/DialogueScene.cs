using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueScene 
{
    public string sceneID;
    public bool startScene;
    public string nextScene;

    bool played = false;
    public List<Dialogue> lines;
        
}
