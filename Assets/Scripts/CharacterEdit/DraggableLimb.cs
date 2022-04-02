using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(mousePos, 0.5f);
        
    }

    private void Start()
    {
        originPosition = transform.position;
    }

    private void Update()
    {

        mousePos = Input.mousePosition;

        //if mouse 0 and in vicinity of the limb
        if (Input.GetMouseButtonDown(0))
        {
            beingHeld = true;

            Cursor.visible = false;
        }


        if (Input.GetMouseButtonUp(0) && beingHeld)
        {
            beingHeld = false;

            Cursor.visible = true;

            if(currSelectedAttachmentPoint != null)
            {
                //we have a transform to use as our attachTo.
                CharacterEditLogic.Instance.UpdateCreature(currSelectedAttachmentPoint, associatedPrefab);

                //CharacterEditLogic.Instance.CameraSnapToFit();

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
