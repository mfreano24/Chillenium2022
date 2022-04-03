using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        //scene transition to first scene
        SceneManager.LoadScene("CombatScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
