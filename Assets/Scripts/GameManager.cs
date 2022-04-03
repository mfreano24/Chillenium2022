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
    public static Action combatStart;
    public float gameSpeed;
    public enum GameState
    {
        PreCombat,
        Combat,
        PostCombat,
        RewardScreen,
        CharacterScreen
    }

    public GameState gameState;
    bool stateProgressing = false;
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
        if (!stateProgressing) 
        {
            switch (gameState)
            {
                case GameState.PreCombat:
                    StartCoroutine( StartCombat());
                    
                    break;
                case GameState.Combat:
                    break;
                case GameState.PostCombat:
                    break;
                case GameState.RewardScreen:
                    break;
                case GameState.CharacterScreen:
                    break;
            }

        }
    }

  IEnumerator StartCombat() 
    {

        combatStart?.Invoke();
        stateProgressing = true;
        gameSpeed = 0;

        //Play Intro CutScene;
        FindObjectOfType<CombatAnimator>().CallIntroAnimations();
        yield return new WaitForSeconds(4.5f);


        gameSpeed = 1;
        stateProgressing = false;
        gameState = GameState.Combat;
        yield return null;

    }
}
