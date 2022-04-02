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

            transform.Rotate(mouseSensitivity * delta);
        }
    }
}
