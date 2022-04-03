using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager m_instance;

    public static AudioManager Instance
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

    public List<Sound> sounds;

    Dictionary<string, AudioClip> sfx;
    private void Start()
    {
        sfx = new Dictionary<string, AudioClip>();
        foreach(Sound s in sounds)
        {
            sfx.Add(s.name, s.clip);
            
        }

    }

    public void PlaySFX(string name)
    {
        AudioSource aud = gameObject.AddComponent<AudioSource>();
        aud.clip = sfx[name];
        aud.Play();

        StartCoroutine(WaitToDestroySource(sfx[name].length, aud));
    }

    IEnumerator WaitToDestroySource(float clipLength, AudioSource src)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(src);
    }


}

[System.Serializable]
public struct Sound
{
    public AudioClip clip;
    public string name;
}
