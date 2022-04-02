using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    List<BodyPart> parts;
    List<bool> visited; //for use in graph searches

    public enum PartType
    {
        Torso, Head, Tail, LArm, RArm, LLeg, RLeg
    }

    private void Start()
    {
        parts = new List<BodyPart>();
        visited = new List<bool>();
    }

    public void ResetVisited()
    {
        visited.Clear();
        for(int i = 0; i < parts.Count; i++)
        {
            visited.Add(false);
        }
    }


    public void AddNewPartToCollection(BodyPart bp)
    {
        bp.Creature = this;
        parts.Add(bp);
    }

    public void ReassignSource(BodyPart newSource)
    {
        if(newSource.BodyPartType != PartType.Torso)
        {
            Debug.LogWarning("Tried to assign a non-torso new source! bad!");
            return;
        }

        
    }
}
