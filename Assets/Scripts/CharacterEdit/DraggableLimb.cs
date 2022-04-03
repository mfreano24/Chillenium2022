using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableLimb : MonoBehaviour
{

    bool beingHeld = false;
    Vector3 mousePos = Vector3.zero;

    [HideInInspector]
    public CreatureManager.PartType thisType;

    Vector3 originPosition;

    public LayerMask attachLM;

    Transform currSelectedAttachmentPoint;


    public GameObject associatedPrefab;

    public Transform debugAttach;

    bool newLimbFound = false;

    public Image cursor;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(mousePos, 0.5f);

    }

    private void Start()
    {
        originPosition = transform.position;
        cursor.sprite = FindObjectOfType<Library>().limbs[GameManager.instance.limRewardLibraryIndex].GetComponent<BodyPart>().bodyPartAbility.partIcon;
    }

    private void Update()
    {
        

        mousePos = Input.mousePosition;

        //if mouse 0 and in vicinity of the limb
        if (Input.GetMouseButtonDown(0))
        {
            beingHeld = true;

            //Cursor.visible = false;
        }


        if (Input.GetMouseButtonUp(0) && beingHeld)
        {
            beingHeld = false;

            //Cursor.visible = true;

            if (currSelectedAttachmentPoint != null)
            {
                //we have a transform to use as our attachTo.

                if (AudioManager.Instance)
                {
                    AudioManager.Instance.PlaySFX("click");
                }
                
                CharacterEditLogic.Instance.UpdateCreature(currSelectedAttachmentPoint,FindObjectOfType<Library>().limbs[GameManager.instance.limRewardLibraryIndex]/*NameToPrefabLibrary.Instance.prefabs[StaticValues.limbPrefabName]*/);
                
                CharacterEditLogic.Instance.CameraSnapToFit();

                CharacterEditLogic.Instance.ProceedCallback(gameObject);
            }
            else
            {
                transform.position = originPosition; //this is the "nowhere" case
            }


        }


        if (beingHeld)
        {
            //snap to mouse position
            transform.position = mousePos;

            //raycast from the screen

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, Mathf.Infinity, attachLM))
            {
                currSelectedAttachmentPoint = hit.collider.gameObject.transform; //this is our attachment point!

            }
            else
            {
                currSelectedAttachmentPoint = null;
            }

        }


    }
}
