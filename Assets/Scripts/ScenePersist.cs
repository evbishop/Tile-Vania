using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : Singleton
{
    public void LevelFinished()
    {
        Destroy(gameObject);
    }
}
