using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BodyPart : MonoBehaviour
{
    //public bool isBase; //this can be used to determine if this body part is allowed to have attachments to it.
    public static BodyPart player;
    
    public bool isSource;
    public bool isPlayer;



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

    //Stored Ability
    public BodyPartAbility bodyPartAbility;
    public GameObject selfPrefab;
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
        if (headAttachmentPoint)
        {
            Gizmos.DrawWireSphere(headAttachmentPoint.position, 0.1f);
        }

        if (tailAttachmentPoint)
        {
            Gizmos.DrawWireSphere(tailAttachmentPoint.position, 0.1f);
        }

        if (lArmAttachmentPoint)
        {
            Gizmos.DrawWireSphere(lArmAttachmentPoint.position, 0.1f);
        }

        if (rArmAttachmentPoint)
        {
            Gizmos.DrawWireSphere(rArmAttachmentPoint.position, 0.1f);
        }

        if (rLegAttachmentPoint)
        {
            Gizmos.DrawWireSphere(rLegAttachmentPoint.position, 0.1f);
        }

        if (lLegAttachmentPoint)
        {
            Gizmos.DrawWireSphere(lLegAttachmentPoint.position, 0.1f);
        }


    }


    private void Awake()
    {
        
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

            if (isPlayer)
            {

                if (GameManager.playerSource == null)
                {
                    GameManager.playerSource = this;
                    GameManager.instance.bodyAbilities.Add(bodyPartAbility);
                    player = this;
                    DontDestroyOnLoad(gameObject);
                    GameManager.instance.AddFunctionToSceneLoadEvent(OnSceneLoaded);
                }
                else if(GameManager.playerSource != this)
                {
                    Destroy(gameObject);
                }

            }
            else
            {
                GameManager.enemySource = this;
            }
        }


    }


    public void OnSceneLoaded()
    {
        if (isSource)
        {
            creature = GetComponent<CreatureManager>();
            depth = 0;
            creature.parts.Add(this);

            if (isPlayer)
            {

                if (GameManager.playerSource == null)
                {
                    GameManager.playerSource = this;
                    player = this;
                    DontDestroyOnLoad(gameObject);
                }
                else if (GameManager.playerSource != this)
                {
                    Destroy(gameObject);
                }

            }
            else
            {
                GameManager.enemySource = this;
            }
        }
        if (!isPlayer && !isSource)
        {
            //GameManager.enemySource.Creature.AddNewPartToCollection(this);
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
        if (BodyPart.player == this) 
        {
            DontDestroyOnLoad(this);
        }
    }

    public void AttachNewBodyPart(Transform attachTo, BodyPart newPart)
    {
        //newPart = pass in the part wanting to be attached to this creature
        //attachTo = pass in the name of the part we're attaching to
        if(newPart == null)
        {
            Debug.Log("new part is null?");
        }

        if(attachTo == null)
        {
            Debug.Log("attach to is null?");
        }
        newPart.transform.parent = attachTo;
        newPart.transform.localPosition = Vector3.zero;
        newPart.transform.localRotation = Quaternion.identity;

        creature.AddNewPartToCollection(newPart);
        newPart.Depth = depth + 1;
        newPart.isPlayer = isPlayer;

    }


    public void DetachBodyPart(string attachTo)
    {
        nameToPart[attachTo].transform.parent = null;
        nameToPart[attachTo].Creature = null;
        nameToPart[attachTo].Depth = -1;
        nameToPart[attachTo] = null;
    }

}
