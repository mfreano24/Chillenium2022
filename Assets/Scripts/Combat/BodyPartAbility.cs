using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyPartAbility
{
    public string name;
    public Sprite partIcon;

    public float maxChargeTime;
    [HideInInspector]
    public float currentChargeTime;
    [Range(0, 2)]

    [HideInInspector]
    public bool isPlayer;
    int abilityIndex;


    public List<AbilityAction> abilityList;

    // Start is called before the first frame update
    public void Init()
    {
        foreach (AbilityAction abilityAction in abilityList)
        {
            abilityAction.parentAbility = this;
            abilityAction.isPlayerOwned = true;
        }

        if (isPlayer)
        {
            abilityIndex = ActionPanel.instance.AddAction(this);
        }
    }

    // Update is called once per frame
    public void AbilityUpdate(float speedMod)
    {
        if (currentChargeTime < maxChargeTime)
        {
            currentChargeTime += Time.deltaTime * GameManager.instance.gameSpeed * (1f + speedMod);
            currentChargeTime = Mathf.Clamp(currentChargeTime, 0, maxChargeTime);
        }
    }

    internal int GetRandomAbility()
    {
        return UnityEngine.Random.Range(0,abilityList.Count);
    }

    public bool IsCharged() 
    {
        return currentChargeTime >= maxChargeTime; 
    }

    internal float GetChargeTimeLeft()
    {
        return maxChargeTime - currentChargeTime;
    }

}
