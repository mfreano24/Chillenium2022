using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionButton : MonoBehaviour
{

    public Image attackIcon;
    AbilityAction abilityAction;
    public void Populate(AbilityAction abilityAction) 
    {
        this.abilityAction = abilityAction;
        attackIcon.sprite = abilityAction.actionIcon;
    }

    public void TriggerAction() 
    {
        abilityAction.UseAction();
        AudioManager.Instance.PlaySFX("punch1");
    }
}
