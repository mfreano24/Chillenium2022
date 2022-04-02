using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    //public bool isBase; //this can be used to determine if this body part is allowed to have attachments to it.
    


    
    public bool isSource;




    //NOTE: THESE SHOULD ONLY BE UTILIZED IF "isBase" IS TRUE
    BodyPart head;
    BodyPart tail;
    BodyPart lArm;
    BodyPart rArm;
    BodyPart lLeg;
    BodyPart rLeg;


    public Transform headAttachmentPoint;
    public Transform tailAttachmentPoint;
    public Transform lArmAttachmentPoint;
    public Transform rArmAttachmentPoint;
    public Transform lLegAttachmentPoint;
    public Transform rLegAttachmentPoint;

    Dictionary<string, BodyPart> nameToPart;

    Dictionary<string, Transform> localPartPosition;

    CreatureManager creature;

    int depth;

    CreatureManager.PartType thisPartType;

    public CreatureManager Creature
    {
        get
        {
            return creature;
        }
        set
        {
            creature = value;
        }
    }

    public int Depth
    {
        get
        {
            return depth;
        }
        set
        {
            depth = value;
        }
    }

    public CreatureManager.PartType BodyPartType
    {
        get
        {
            return thisPartType;
        }
        set
        {
            thisPartType = value;
        }
    }

    

    //body part's RPG stats and actions should be a separate class i think

    private void OnDrawGizmos()
    {
        //draw the "attachment points" locally
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(headAttachmentPoint.position, 1f);
        Gizmos.DrawWireSphere(tailAttachmentPoint.position, 1f);
        Gizmos.DrawWireSphere(lArmAttachmentPoint.position, 1f);
        Gizmos.DrawWireSphere(rArmAttachmentPoint.position, 1f);
        if (!isSource)
        {
            Gizmos.DrawWireSphere(lLegAttachmentPoint.position, 1f);
            Gizmos.DrawWireSphere(rLegAttachmentPoint.position, 1f);
        }
        

    }



    private void Start()
    {
        nameToPart = new Dictionary<string, BodyPart>();
        localPartPosition = new Dictionary<string, Transform>();
        
        InitializeNameToPart();


        if (isSource)
        {
            creature = GetComponent<CreatureManager>();
            depth = 0;
            creature.parts.Add(this);
        }
    }

    void InitializeNameToPart()
    {
        nameToPart.Add("head", head);
        nameToPart.Add("tail", tail);
        nameToPart.Add("lArm", lArm);
        nameToPart.Add("rArm", rArm);
        if (!isSource)
        {
            nameToPart.Add("lLeg", lLeg);
            nameToPart.Add("rLeg", rLeg);
        }
        

        ///////

        localPartPosition.Add("head", headAttachmentPoint);
        localPartPosition.Add("tail", tailAttachmentPoint);
        localPartPosition.Add("lArm", lArmAttachmentPoint);
        localPartPosition.Add("rArm", rArmAttachmentPoint);
        if (!isSource)
        {
            localPartPosition.Add("lLeg", lLegAttachmentPoint);
            localPartPosition.Add("rLeg", rLegAttachmentPoint);
        }
        
    }

    private void Update()
    {

    }

    public void AttachNewBodyPart(Transform attachTo, BodyPart newPart)
    {
        //newPart = pass in the part wanting to be attached to this creature
        //attachTo = pass in the name of the part we're attaching to
        newPart.transform.parent = attachTo;
        newPart.transform.localPosition = Vector3.zero;
        newPart.transform.localRotation = Quaternion.identity;

        creature.AddNewPartToCollection(newPart);
        newPart.Depth = depth + 1;

    }


    public void DetachBodyPart(string attachTo)
    {
        nameToPart[attachTo].transform.parent = null;
        nameToPart[attachTo].Creature = null;
        nameToPart[attachTo].Depth = -1;
        nameToPart[attachTo] = null;
    }
}
