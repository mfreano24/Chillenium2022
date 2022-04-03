using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("Icon ID")]
    public int iconID;
    [Header("SFX ID")]
    public int sfxID;
    [TextArea(5,5)]
    public string line;
    //public List<Sprite> spriteAtlas;
}
