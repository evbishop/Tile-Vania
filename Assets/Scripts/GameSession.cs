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

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int value)
    {
        score += value;
        if (scoreText) scoreText.text = score.ToString();
    }

    public IEnumerator PlayerDied()
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
            FindObjectOfType<ScenePersist>().LevelFinished();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
    
    public void GameRestared()
    {
        Destroy(gameObject);
    }
}
