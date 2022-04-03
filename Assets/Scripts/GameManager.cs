using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    public List<BodyPartAbility> bodyAbilities;

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
    public bool stateProgressing = false;

    public GameObject limbRewardPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("already a gamemanager in here, deleting this one!");
            Destroy(this);
        }
        else
        {

            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!stateProgressing)
        {
            switch (gameState)
            {
                case GameState.PreCombat:
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
                        StartCombat();
                    }
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

    public void AddFunctionToSceneLoadEvent(Action pass)
    {
        SceneManager.sceneLoaded += delegate { pass(); };
    }


    void StartCombat()
    {
        FindObjectOfType<CombatAnimator>().CallIntroAnimations();
        stateProgressing = true;
        gameSpeed = 0;


        try
        {
            CombatActorManager[] combatActorManagers = FindObjectsOfType<CombatActorManager>();
            combatActorManagers[0].Init();
            combatActorManagers[1].Init();
        }
        catch (Exception)
        {
            stateProgressing = false;
            throw;
        }


        gameSpeed = 1;
        stateProgressing = false;
        gameState = GameState.Combat;

        return;

    }

    internal void LoseGame()
    {
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

    internal void GotoCombatNoPlayer()
    {
        SceneManager.LoadScene(0);
        gameState = GameState.PreCombat;
    }

    internal void GotoCombat()
    {
        SceneManager.LoadScene(0);
        gameState = GameState.PreCombat;

    }
}
