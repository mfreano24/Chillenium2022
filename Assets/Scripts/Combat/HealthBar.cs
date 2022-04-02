using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthBar;
    public Image armorBar;
    public bool isPlayer;
    CombatActorManager creature;
    private void Start()
    {
        creature = isPlayer ? GameManager.instance.player : GameManager.instance.enemy;
    }
    // Update is called once per frame
    void Update()
    {

        healthBar.fillAmount =Mathf.Clamp( creature.displayHealth / creature.maxHealth,0,1f);
        armorBar.fillAmount = Mathf.Clamp(creature.actualArmor / creature.maxHealth, 0, 1f);
    }
}
