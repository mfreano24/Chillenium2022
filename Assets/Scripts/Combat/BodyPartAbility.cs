using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartAbility : MonoBehaviour
{
    public float maxChargeTime;
    [HideInInspector]
    public float currentChargeTime;
    [Range(0, 2)]
    public int abilityCount;
    public bool isPlayer;

    List<AbilityAction> abilityList;
    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            ActionPanel.instance.AddAction(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentChargeTime < maxChargeTime)
        {
            currentChargeTime += Time.deltaTime;
            maxChargeTime = Mathf.Clamp(maxChargeTime, 0, currentChargeTime);
        }

    }
}
