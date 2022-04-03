using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSelect : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> options;
    List<BodyPart> eligibleBodyParts;
    List<BodyPart> selectedBodyParts;

    // Start is called before the first frame update
    void Start()
    {
        BodyPart[] bodyParts = FindObjectsOfType<BodyPart>();
        eligibleBodyParts = new List<BodyPart>();
        selectedBodyParts = new List<BodyPart>();


        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (!bodyParts[i].isPlayer)
            {
                eligibleBodyParts.Add(bodyParts[i]);
            }
        }


        for (int i = 0; i < 3; i++)
        {
            if (eligibleBodyParts.Count > 0)
            {
                int selection = Random.Range(0, eligibleBodyParts.Count);
                selectedBodyParts.Add(eligibleBodyParts[selection]);
                eligibleBodyParts.RemoveAt(selection);

            }
        }
        if (selectedBodyParts.Count < 3) 
        {
            options[2].SetActive(false);
        }
        if (selectedBodyParts.Count < 2)
        {
            options[1].SetActive(false);
        }
        
        for (int i = 0; i < selectedBodyParts.Count; i++) 
        {
            options[i].GetComponent<GiftSelectChoice>().Populate(selectedBodyParts[i]);
        }
    }
}
