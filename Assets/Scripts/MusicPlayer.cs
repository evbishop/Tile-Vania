using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : Singleton
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip cave, victory;

    void Start()
    {
        LevelExit.OnVictoryScreenReached += HandleVictoryScreenReached;
        Menu.OnLoadMainMenu += HandleLoadingMainMenu;
    }

    void OnDestroy()
    {
        LevelExit.OnVictoryScreenReached -= HandleVictoryScreenReached;
        Menu.OnLoadMainMenu += HandleLoadingMainMenu;
    }

    void HandleVictoryScreenReached()
    {
        audioSource.clip = victory;
        audioSource.Play();
    }

    void HandleLoadingMainMenu()
    {
        audioSource.clip = cave;
        audioSource.Play();
    }
}
