using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour
{
    // Prevent Cloud Spawner from unloading when game starts
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}