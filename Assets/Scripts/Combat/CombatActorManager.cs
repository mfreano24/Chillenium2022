using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActorManager : MonoBehaviour
{
    public bool isPlayer;
    public List<BodyPartAbility> bodyPartAbilities;
    public float maxHealth;
    public float actualHealth;
    public float displayHealth;

    public float actualArmor;

    public float healthAnimationSpeed;
    AttackUIElement nextAttackDisplay;
    BodyPartAbility nextAbility;
    int nextAction;
    float damageBoost;
    int parryActive;
    int psychicHazes;
    float chargeSpeedMod;
    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            foreach (BodyPartAbility bodyPartAbility in bodyPartAbilities)
            {
                bodyPartAbility.isPlayer = true;
                bodyPartAbility.Init();

            }
        }
        else
        {
            SetAIAbility();
            nextAction = new int();
        }

        displayHealth = actualHealth;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BodyPartAbility bodyPartAbility in bodyPartAbilities)
        {

            bodyPartAbility.AbilityUpdate(chargeSpeedMod);
        }
        if (!isPlayer)
        {
            if (nextAbility != null)
            {
                if (nextAbility.IsCharged())
                {
                    UseQueuedAIAbility();
                }
            }
        }
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (displayHealth != actualHealth)
        {
            displayHealth += Mathf.Sign(actualHealth - displayHealth) * healthAnimationSpeed * Time.deltaTime;
            if (Mathf.Abs(actualHealth - displayHealth) <= .5f)
            {
                displayHealth = actualHealth;
            }
        }
    }

    BodyPartAbility FindClosestAbility()
    {
        float maxDif = float.MaxValue;
        BodyPartAbility nextBodyPartAbility = bodyPartAbilities[0];
        foreach (BodyPartAbility bodyPartAbility in bodyPartAbilities)
        {

            float chargeTime = bodyPartAbility.GetChargeTimeLeft();
            if (chargeTime < maxDif)
            {
                maxDif = chargeTime;
                nextBodyPartAbility = bodyPartAbility;
            }

        }

        return nextBodyPartAbility;

    }



    void SetAIAbility()
    {
        BodyPartAbility closestAbility = FindClosestAbility();
        nextAbility = closestAbility;
        nextAction = closestAbility.GetRandomAbility();
        NextEnemyAttack.instance.Populate(closestAbility, nextAction);
    }



    void UseQueuedAIAbility()
    {
        nextAbility.abilityList[nextAction].UseAction();
        nextAbility.currentChargeTime = 0;
        SetAIAbility();
    }

    internal void Heal(float healAmount)
    {
        actualHealth += healAmount;
        actualHealth = Mathf.Clamp(actualHealth, 0, maxHealth);
    }

    internal void Damage(float damage)
    {

        if (parryActive > 0) 
        {
            damage = 0;
        }

        float damageToDo = (damage * (1f + damageBoost));

        if (actualArmor > 0)
        {

            actualArmor -= damageToDo;
            if (actualArmor < 0)
            {
                damageToDo = -actualArmor;
                actualArmor = 0;
            }
            else 
            {
                damageToDo = 0;
            }
        }

        actualHealth -= (damageToDo);
        actualHealth = Mathf.Clamp(actualHealth, 0, maxHealth);
    }
    internal void AddArmor(float armor) 
    {
        actualArmor += armor;
        actualArmor = Mathf.Clamp(actualArmor, 0, maxHealth);
    }
    internal void ActivateParry(float parryWindow)
    {
        StartCoroutine(StartParry(parryWindow));
    }

    public void CallSpeedModAdjust(float speedMod, float adjustTime)
    {

        StartCoroutine(SpeedModeAdjust(speedMod,adjustTime));

    }

    public void CallDamageOverTime(float damage, float damageInterval, float damageTicks)
    {

        StartCoroutine(DamageOverTime(damage, damageInterval, damageTicks));

    }

    public void CallHealOverTime(float healAmount, float healInterval, float healTicks)
    {

        StartCoroutine(HealOverTime(healAmount, healInterval, healTicks));

    }

    public void CallSetTempDamageModifier(float damageModifier, float damageModifierTime)
    {

        StartCoroutine(SetTempDamageModifier(damageModifier, damageModifierTime));

    }

    IEnumerator SpeedModeAdjust(float speedMod, float adjustTime) 
    {
        chargeSpeedMod += speedMod;
        yield return new WaitForSeconds(adjustTime);
        chargeSpeedMod -= speedMod;
    
    }

    IEnumerator DamageOverTime(float damage, float damageInterval, float damageTicks)
    {
        for (int i = 0; i < damageTicks; i++)
        {

            Damage(damage);
            yield return new WaitForSeconds(damageInterval);
        }

    }

    IEnumerator HealOverTime(float healAmount, float healInterval, float healTicks)
    {
        for (int i = 0; i < healTicks; i++)
        {

            Heal(healAmount);
            yield return new WaitForSeconds(healInterval);
        }

    }

    IEnumerator SetTempDamageModifier(float damageModifier, float damageModifierTime)
    {
        damageBoost += damageModifier;
        yield return new WaitForSeconds(damageModifierTime);
        damageBoost -= damageModifier;
    }

    IEnumerator StartParry(float parryWindow)
    {
        parryActive++;
        yield return new WaitForSeconds(parryWindow);
        parryActive--;

    }
}
