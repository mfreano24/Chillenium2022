using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextEnemyAttack : MonoBehaviour
{
    public static NextEnemyAttack instance;
    public AttackUIElement attackUIElement;

    private void Awake()
    {
        instance = this;
    }
    public void Populate(BodyPartAbility bodyPartAbility, int actionIndex) 
    {
        attackUIElement.Populate(bodyPartAbility, actionIndex);
    }

    internal void ClearActions()
    {
        attackUIElement.ClearActionIcons();
    }
}
