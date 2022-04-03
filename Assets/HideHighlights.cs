using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHighlights : MonoBehaviour
{
    Highlight[] highlights = FindObjectsOfType<Highlight>();
    private void Start()
    {
        foreach (Highlight highlight in highlights)
        {
            highlight.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
