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
        StaticValues.limbPrefabName = bodyPart.selfPrefab.name;
    }

    public void AcceptGift()
    {
        //StaticValues.limbPrefabName = giftChoice.name;
        GameManager.playerSource.gameObject.transform.parent = null;
        DontDestroyOnLoad(GameManager.playerSource.gameObject);
        Debug.Log(StaticValues.limbPrefabName);
        GameManager.instance.GoToCharacterBuildScreen();
    }
}


