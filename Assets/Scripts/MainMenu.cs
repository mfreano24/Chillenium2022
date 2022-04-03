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
        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlaySFX("click");
        }

        if (MusicManager.Instance)
        {
            MusicManager.Instance.SetBattleMode(true);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlaySFX("click");
        }
    }
}
