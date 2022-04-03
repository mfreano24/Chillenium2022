using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GiftSelectChoice : MonoBehaviour
{

    public TextMeshProUGUI textDispaly;
    GameObject giftChoice;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Populate(BodyPart bodyPart)
    {
        textDispaly.text = bodyPart.bodyPartAbility.name;
        giftChoice = bodyPart.selfPrefab;
    }

    public void AcceptGift() 
    {
        GameManager.instance.limbRewardPrefab = giftChoice;

        Debug.Log(giftChoice.name);
        GameManager.instance.GoToCharacterBuildScreen();
    }
}


