using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityAction
{
    public string name;
    public Sprite actionIcon;

    [HideInInspector]
    public BodyPartAbility parentAbility;
    [HideInInspector]
    public bool isPlayerOwned = false;

    public enum ActionType
    {
        StraightDamage,
        Healing,
        DamageOverTime,
        HealOverTime,
        AttackUp,
        DefenseUp,
        ArmorUp,
        ParryHit,
        //PsychicHaze,
        SpeedCharge,
        SlowCharge,
        //GhostlyAssistance

    }
    public enum ActionTarget
    {
        Self,
        Enemy
    }
    public ActionType actionType;

    public float valueOne;
    public float valueTwo;
    public float valueThree;


    public virtual void UseAction()
    {
        if (parentAbility.IsCharged())
        {
            parentAbility.currentChargeTime = 0;
            DoAction();
        }
    }
    public virtual void DoAction()
    {
        switch (actionType)
        {
            case ActionType.StraightDamage:
                DamageEnemy(valueOne);
                break;
            case ActionType.Healing:
                HealSelf(valueOne);
                break;
            case ActionType.DamageOverTime:
                DamageOverTime(valueOne, valueTwo, valueThree);
                break;
            case ActionType.HealOverTime:
                HealOverTime(valueOne, valueTwo, valueThree);
                break;
            case ActionType.AttackUp:
                AttackUp(valueOne, valueTwo);
                break;
            case ActionType.DefenseUp:
                DefenseUp(valueOne, valueTwo);
                break;
            case ActionType.ParryHit:
                ActivateParry(valueOne);
                break;
            //case ActionType.PsychicHaze:
            //    break;
            case ActionType.SpeedCharge:
                SpeedModSelf(valueOne, valueTwo);
                break;
            case ActionType.SlowCharge:
                SpeedModEnemy(valueOne, valueTwo);
                break;
            //case ActionType.GhostlyAssistance:
            //    break;
            case ActionType.ArmorUp:
                AddArmor(valueOne);
                break;
            default:
                break;
        }

    }


    private void SpeedModSelf(float valueOne, float valueTwo)
    {
        CombatActorManager target = PickTarget(ActionTarget.Self);
        target.CallSpeedModAdjust(valueOne, valueTwo);
    }

    private void SpeedModEnemy(float valueOne, float valueTwo)
    {
        CombatActorManager target = PickTarget(ActionTarget.Enemy);
        target.CallSpeedModAdjust(valueOne, valueTwo);
    }

    private void ActivateParry(float parryWindow)
    {
        CombatActorManager target = PickTarget(ActionTarget.Self);
        target.ActivateParry(parryWindow);
    }

    private void AddArmor(float armor)
    {
        CombatActorManager target = PickTarget(ActionTarget.Self);
        target.AddArmor(armor);
    }

    private void DefenseUp(float damageModifier, float modifierTime)
    {
        CombatActorManager target = PickTarget(ActionTarget.Self);
        target.CallSetTempDamageModifier(damageModifier, modifierTime);
    }

    private void AttackUp(float damageModifier, float modifierTime)
    {
        CombatActorManager target = PickTarget(ActionTarget.Enemy);
        target.CallSetTempDamageModifier(damageModifier, modifierTime);
    }

    void DamageEnemy(float damage)
    {
        CombatActorManager target = PickTarget(ActionTarget.Enemy);
        Debug.Log(target.name + " is dealt " + damage);
        target.Damage(damage);

    }



    void HealSelf(float healAmount)
    {
        CombatActorManager target = PickTarget(ActionTarget.Self);

        target.Heal(healAmount);
    }

    void DamageOverTime(float damage, float interval, float ticks)
    {

        CombatActorManager target = PickTarget(ActionTarget.Enemy);
        target.CallDamageOverTime(damage, interval, ticks);

    }

    void HealOverTime(float healAmount, float interval, float ticks)
    {
        CombatActorManager target = PickTarget(ActionTarget.Self);
        target.CallDamageOverTime(healAmount, interval, ticks);
    }




    private CombatActorManager PickTarget(ActionTarget actionTarget)
    {
        CombatActorManager target;

        if (actionTarget == ActionTarget.Enemy)
        {

            target = isPlayerOwned ? GameManager.instance.enemy : GameManager.instance.player;

        }
        else
        {
            target = isPlayerOwned ? GameManager.instance.player : GameManager.instance.enemy;
        }
        return target;
    }
}
