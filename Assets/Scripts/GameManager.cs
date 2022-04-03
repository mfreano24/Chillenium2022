using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static BodyPart playerSource;
    public static BodyPart enemySource;

    public CombatActorManager player;
    public CombatActorManager enemy;
    public static Action  combatStart;

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
        if (Input.GetButtonDown("Jump")) 
        {
            StartCombat();
        }
    }

    public void StartCombat() 
    {
        combatStart?.Invoke();
    }
}
