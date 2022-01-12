using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    void Awake()
    {
        int numOfScenePersists = FindObjectsOfType(GetType()).Length;
        if (numOfScenePersists > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
}
