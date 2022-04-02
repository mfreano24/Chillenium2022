using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyTestScript : MonoBehaviour
{
    [Header("Parts")]
    public BodyPart head;
    public BodyPart tail;
    public BodyPart lArm;
    public BodyPart rArm;
    public BodyPart lLeg;
    public BodyPart rLeg;

    [Header("Base")]
    public BodyPart baseTorso;

    private void Start()
    {
        StartCoroutine(WaitExecute());
    }


    IEnumerator WaitExecute()
    {
        //test code here
        yield return new WaitForSeconds(1.0f);

        baseTorso.AttachNewBodyPart("head", head);
        baseTorso.AttachNewBodyPart("tail", tail);
        baseTorso.AttachNewBodyPart("lArm", lArm);
        baseTorso.AttachNewBodyPart("rArm", rArm);
        baseTorso.AttachNewBodyPart("lLeg", lLeg);
        baseTorso.AttachNewBodyPart("rLeg", rLeg);

    }
}
