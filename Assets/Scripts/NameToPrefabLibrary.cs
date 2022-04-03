using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameToPrefabLibrary : MonoBehaviour
{
    static NameToPrefabLibrary m_instance;

    public static NameToPrefabLibrary Instance
    {
        get
        {
            return m_instance;
        }
    }

    public List<NamedPrefab> plist;

    public Dictionary<string, GameObject> prefabs;

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogError("Already a NameToPrefabLibrary in this scene!!");
            Destroy(this);
        }

        m_instance = this;

        prefabs = new Dictionary<string, GameObject>();
        foreach (NamedPrefab np in plist)
        {
            Debug.Log("name: " + np.name + ", prefab: " + np.prefab.name);
            prefabs.Add(np.name, np.prefab);
        }
    }


    
    private void Start()
    {
        
    }

}

[System.Serializable]
public struct NamedPrefab
{
    public string name;
    public GameObject prefab;
}
