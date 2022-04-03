using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUIElement : MonoBehaviour
{

    public BodyPartAbility bodyPartAbility;
    public Image bodyIcon;
    public Image fillBar;
    public Transform buttonHolder;
    public GameObject attackButton;

    // Update is called once per frame
    void Update()
    {
        if (bodyPartAbility != null)
        {
            fillBar.fillAmount = Mathf.Clamp(bodyPartAbility.currentChargeTime / bodyPartAbility.maxChargeTime, 0, 1f);
        }
    }

    public void Populate(BodyPartAbility bodyPartReference)
    {
        bodyPartAbility = bodyPartReference;

        bodyIcon.sprite = bodyPartReference.partIcon;

        foreach (AbilityAction abilityAction in bodyPartReference.abilityList)
        {
            GameObject instantiatedActionButton = Instantiate(attackButton, buttonHolder);
            instantiatedActionButton.GetComponent<ActionButton>().Populate(abilityAction);
        }
    }

    public void Populate(BodyPartAbility bodyPartReference, int actionIndex)
    {
        bodyPartAbility = bodyPartReference;

        bodyIcon.sprite = bodyPartReference.partIcon;

        Debug.Log("1");
        Debug.Log(actionIndex);
        Debug.Log(bodyPartReference.abilityList.Count);
        AbilityAction abilityAction = bodyPartReference.abilityList[actionIndex];
        Debug.Log("2");
        if (buttonHolder.transform.childCount == 0)
        {
            Debug.Log("3");
            GameObject instantiatedActionButton = Instantiate(attackButton, buttonHolder);
            instantiatedActionButton.GetComponent<ActionButton>().Populate(abilityAction);
        }
        else 
        {
            Debug.Log("4");
            buttonHolder.GetChild(0).GetComponent<ActionButton>().Populate(abilityAction);
        }

    }
    public void ClearActionIcons()
    {
        foreach (Transform child in buttonHolder)
        {
            Destroy(child);
        }
    }
}
