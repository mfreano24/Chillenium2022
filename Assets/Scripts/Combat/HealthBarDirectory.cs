using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDirectory : MonoBehaviour
{

    public static HealthBarDirectory instance;

    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;

    private void Awake()
    {
        instance = this;
    }

}
