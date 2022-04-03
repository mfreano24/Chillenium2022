using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomizer : MonoBehaviour
{

    public List<GameObject> Enemies; 
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(Enemies[Random.Range(0, Enemies.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
