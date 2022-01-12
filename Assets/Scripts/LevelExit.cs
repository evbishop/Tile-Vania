using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f, levelExitSlowMo = 0.2f;

    public static event Action OnLevelFinished;
    public static event Action OnVictoryScreenReached;

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowMo;
        yield return new WaitForSeconds(levelLoadDelay);
        Time.timeScale = 1f;

        OnLevelFinished?.Invoke();

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        print(nextSceneIndex);
        print(SceneManager.sceneCountInBuildSettings);
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            OnVictoryScreenReached?.Invoke();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
