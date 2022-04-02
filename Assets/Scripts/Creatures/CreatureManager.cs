using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{

    [HideInInspector] public List<BodyPart> parts;
    List<bool> visited; //for use in graph searches


    BodyPart source;

    public enum PartType
    {
        Torso, Head, Tail, LArm, RArm, LLeg, RLeg
    }

    private void Start()
    {
        parts = new List<BodyPart>();
        visited = new List<bool>();

        source = GetComponent<BodyPart>();
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

    public List<float> GetBoundingValues()
    {
        //run this on dropping 
        List<float> boundValues = new List<float>();
        //minX maxX minY maxY
        boundValues.Add(float.MaxValue);
        boundValues.Add(float.MinValue);
        boundValues.Add(float.MaxValue);
        boundValues.Add(float.MinValue);

        foreach(BodyPart p in parts)
        {
            Debug.Log("PART POSITION: " + p.transform.position);
            if(p.transform.position.x < boundValues[0])
            {
                boundValues[0] = p.transform.position.x;
            }

            if (p.transform.position.x > boundValues[1])
            {
                boundValues[1] = p.transform.position.x;
            }

            if (p.transform.position.y < boundValues[2])
            {
                boundValues[2] = p.transform.position.y;
            }

            if (p.transform.position.y > boundValues[3])
            {
                boundValues[3] = p.transform.position.y;
            }
        }

        return boundValues;
    }
}
