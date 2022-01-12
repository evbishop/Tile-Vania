using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinValue = 100;
    bool coinEnabled = true;
    public static event Action<int> OnCoinTaken;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!coinEnabled) return;
        coinEnabled = false;
        OnCoinTaken?.Invoke(coinValue);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position, 0.5f);
    }
}
