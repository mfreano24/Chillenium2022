using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CombatActorManager player;
    public CombatActorManager enemy;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("already a gamemanager in here, deleting this one!");
            Destroy(this);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
