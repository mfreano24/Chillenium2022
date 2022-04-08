using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHighlights : MonoBehaviour
{
    Highlight[] highlights;
    private void Update()
    {
        highlights = FindObjectsOfType<Highlight>();
        foreach (Highlight highlight in highlights)
        {
            highlight.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
