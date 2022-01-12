using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Rigidbody2D rb;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.localScale.x > 0) rb.velocity = new Vector2(moveSpeed, 0f);
        else rb.velocity = new Vector2(-moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        HorizontalFlip();
    }

    private void HorizontalFlip()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
    }
}
