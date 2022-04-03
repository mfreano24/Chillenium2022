using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
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
        CharacterScreen,
        LostGame
    }

    public GameState gameState;
    bool stateProgressing = false;

    public GameObject limbRewardPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("already a gamemanager in here, deleting this one!");
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stateProgressing) 
        {
            switch (gameState)
            {
                case GameState.PreCombat:
                    StartCoroutine(StartCombat());
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

        stateProgressing = true;
        gameSpeed = 0;
        //Play Intro CutScene;
        FindObjectOfType<CombatAnimator>().CallIntroAnimations();
        CombatActorManager[] combatActorManagers = FindObjectsOfType<CombatActorManager>();
        combatActorManagers[0].Init();
        combatActorManagers[1].Init();

        //foreach (CombatActorManager combatActorManager in combatActorManagers) 
        //{
        //    combatActorManager.Init();
        //}
        yield return new WaitForSeconds(5.5f);


        gameSpeed = 1;
        stateProgressing = false;
        gameState = GameState.Combat;
        yield return null;

    }

    internal void LoseGame()
    {
        stateProgressing = true;
        gameState = GameState.PostCombat;
        gameSpeed = 0;

        FindObjectOfType<CombatAnimator>().CallLossAnimations();


        StartCoroutine(GoToLoseScreen(2.5f));
    }

    IEnumerator GoToLoseScreen(float delay) 
    {
        yield return new WaitForSeconds(delay);
        gameState = GameState.LostGame;
        SceneManager.LoadScene(1);
    }

    internal void WinRound()
    {

        gameState = GameState.PostCombat;
        gameSpeed = 0;
        stateProgressing = true;

        FindObjectOfType<CombatAnimator>().CallWinAnimations();
    }


    public void GoToCharacterBuildScreen() 
    {
        StartCoroutine(GoToBuildScreen(2.5f));
        FindObjectOfType<CombatAnimator>().CallFadeOut();
    }

    IEnumerator GoToBuildScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameState = GameState.CharacterScreen;
        SceneManager.LoadScene(2);
    }

    internal void GotoCombat()
    {
        SceneManager.LoadScene(0);
        gameState = GameState.PreCombat;
        stateProgressing = true;
        throw new NotImplementedException();
    }
}
