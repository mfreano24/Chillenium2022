using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAllDots : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Highlight[] highlights = FindObjectsOfType<Highlight>();
        foreach (Highlight highlight in highlights) 
        {
            highlight.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
