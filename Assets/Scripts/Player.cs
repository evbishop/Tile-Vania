using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f, jumpSpeed = 5f, climbSpeed=5f;
    [SerializeField] float deathKickX = 10f, deathKickY = 10f;

    bool isAlive = true;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;

    public static event Action OnPlayerDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            anim.SetTrigger("death");
            if (transform.localScale.x > 0) rb.velocity = new Vector2(-deathKickX, deathKickY);
            else rb.velocity = new Vector2(deathKickX, deathKickY);
            isAlive = false;
            OnPlayerDeath?.Invoke();
        }
    }

    void Run()
    {
        rb.velocity = new Vector2(runSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("running", playerHasHorizontalSpeed);
    }

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            anim.SetBool("climbing", false);
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb.velocity.x, controlThrow * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        anim.SetBool("climbing", playerHasVerticalSpeed);
    }

    void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (Input.GetButtonDown("Jump")) 
            rb.velocity += new Vector2(0f, jumpSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
    }
}
