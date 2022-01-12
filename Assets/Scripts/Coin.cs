using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinValue = 100;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
        FindObjectOfType<GameSession>().IncreaseScore(coinValue);
        Destroy(gameObject);
    }
}
