using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numOfScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numOfScenePersists > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
    public void LevelFinished()
    {
        Destroy(gameObject);
    }
}
