using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager m_instance;

    public static MusicManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogError("Already a AudioManager in this scene!!");
        }

        m_instance = this;

        DontDestroyOnLoad(this);
    }

    bool inBattle = true;

    private void Start()
    {
        
    }

    public AudioSource BattleMusic;
    public AudioSource MenuMusic;

    public void SetBattleMode(bool _batt)
    {
        bool opposite = (_batt != inBattle);
        inBattle = _batt;
        if (opposite)
        {
            StartCoroutine(FadeMusic());
        }
        
    }

    IEnumerator FadeMusic()
    {
        if (inBattle)
        {
            for(int i = 0; i < 15; i++)
            {
                MenuMusic.volume -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
            MenuMusic.volume = 0.0f;
            BattleMusic.volume = 0.75f;
            BattleMusic.Play();
        }
        else
        {
            for (int i = 0; i < 15; i++)
            {
                BattleMusic.volume -= 0.05f;
                MenuMusic.volume += 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
            BattleMusic.volume = 0.0f;
        }
    }
}
