using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : Singleton
{
    [SerializeField] int playerLives = 3, score = 0;
    [SerializeField] float delayAfterDeath = 1f;
    [SerializeField] Text livesText, scoreText;

    public static event Action OnFinalDeath;

    void Start()
    {
        Menu.OnLoadMainMenu += SessionOver;
        Coin.OnCoinTaken += IncreaseScore;
        Player.OnPlayerDeath += HandlePlayerDeath;
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    void OnDestroy()
    {
        Menu.OnLoadMainMenu -= SessionOver;
        Coin.OnCoinTaken -= IncreaseScore;
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

    void SessionOver()
    {
        Destroy(gameObject);
    }

    void IncreaseScore(int value)
    {
        score += value;
        if (scoreText) scoreText.text = score.ToString();
    }

    void HandlePlayerDeath()
    {
        StartCoroutine(PlayerDied());
    }

    IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(delayAfterDeath);
        if (playerLives > 1)
        {
            playerLives--;
            livesText.text = playerLives.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            OnFinalDeath?.Invoke();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
