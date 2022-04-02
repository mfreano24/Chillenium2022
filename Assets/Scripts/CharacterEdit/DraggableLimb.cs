using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableLimb : MonoBehaviour
{
    bool beingHeld = false;
    Vector3 mousePos = Vector3.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(mousePos, 0.5f);
        
    }

    private void Update()
    {
         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("Distance from limb to mouse: " + Vector3.Distance(mousePos, transform.position));

        //if mouse 0 and in vicinity of the limb
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(mousePos, transform.position) < 1.0f)
        {
            beingHeld = true;

            Cursor.visible = false;
        }


        if (Input.GetMouseButtonUp(0) && beingHeld)
        {
            beingHeld = false;

            Cursor.visible = true;
        }


        if (beingHeld)
        {
            //snap to mouse position
        }
    }
}
