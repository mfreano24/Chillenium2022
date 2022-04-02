using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPanel : MonoBehaviour
{
    public static ActionPanel instance;
    List<BodyPartAbility> abilityList;
    public GameObject actionPanelPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        abilityList = new List<BodyPartAbility>();
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {    }

    public int AddAction( BodyPartAbility bodyPartAbility) 
    {

        int id = abilityList.Count;
        abilityList.Add(bodyPartAbility);
        GameObject instantiatedPrefab = Instantiate(actionPanelPrefab, this.transform);
        instantiatedPrefab.GetComponent<AttackUIElement>().Populate(bodyPartAbility);
        return id;


    }
}
