using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEditLogic : MonoBehaviour
{
    private static CharacterEditLogic m_instance;

    public static CharacterEditLogic Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        if(m_instance != null)
        {
            Debug.Log("Deleting this CharacterEditLogic, already one in scene!!!");
            Destroy(this);
        }

        m_instance = this;
    }


    bool rotatingMesh = false;

    public float mouseSensitivity;

    Vector3 mouseOrigin; //updated every frame so we update deltas

    public Transform creatureTransform;

    public CreatureManager src;

    public bool ConfirmSelection = false;

    public GameObject AreYouSureMenu;

    private void Start()
    {
        src = GameObject.FindGameObjectWithTag("Source").GetComponent<CreatureManager>(); //sorry
        //im not actually that sorry

        AreYouSureMenu.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //begin rotating with mouse
            rotatingMesh = true;
            mouseOrigin = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(1) && rotatingMesh)
        {
            rotatingMesh = false;
        }


        if (rotatingMesh)
        {
            Vector3 delta = Input.mousePosition - mouseOrigin;

            
            delta.y = -delta.x;
            delta.x = 0.0f;
            delta.z = 0.0f;

            creatureTransform.Rotate(mouseSensitivity * delta);

            mouseOrigin = Input.mousePosition;
        }
    }

    public void DiscardLimb()
    {
        //no change, move to next scene
    }

    public void UpdateCreature(Transform attachTo, GameObject limbPrefab)
    {
        //create a limb object from the prefab
        GameObject newLimbInst = Instantiate(limbPrefab);

        //need to get the "parent" of the attach point
        BodyPart targetSource = attachTo.GetComponent<AttachmentPoint>().myParent;
        targetSource.AttachNewBodyPart(attachTo, newLimbInst.GetComponent<BodyPart>());
    }

    public void CameraSnapToFit()
    {
        //calculate bounds of the whole mesh
        List<float> boundingBox = src.GetBoundingValues();
        float xSize = boundingBox[1] - boundingBox[0];
        float ySize = boundingBox[3] - boundingBox[2];

        float adjust = Mathf.Max(xSize, ySize);

        Camera.main.orthographicSize = adjust;
    }

    public void ProceedCallback(GameObject limbObj)
    {
        limbObj.SetActive(false);
        StartCoroutine(Proceed());
    }

    IEnumerator Proceed()
    {
        //"are you sure?"
        //pop that menu up here

        yield return new WaitForSeconds(1.25f);

        AreYouSureMenu.SetActive(true);

        yield return new WaitUntil(() => ConfirmSelection);

        //TODO:move to next scene from here
    }

    public void SelectYes()
    {
        ConfirmSelection = true;

    }
    public void SelectNo()
    {
        //need to remove the part just added
    }
}
