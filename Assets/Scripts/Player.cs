using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f, jumpSpeed = 28f, climbSpeed=5f;
    [SerializeField] float deathKickX = 10f, deathKickY = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] CapsuleCollider2D bodyCollider;
    [SerializeField] BoxCollider2D feetCollider;
    bool isAlive = true, playerHasHorizontalSpeed;
    float gravityScaleOffLadder;

    public static event Action OnPlayerDeath;

    void Start()
    {
        gravityScaleOffLadder = rb.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return;
        playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        Run();
        ClimbLadder();
        if (playerHasHorizontalSpeed) FlipSprite();
        if (Input.GetButtonDown("Jump")
            && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) Jump();
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) Die();
    }

    void Die()
    {
        anim.SetTrigger("death");
        if (transform.localScale.x > 0) rb.velocity = new Vector2(-deathKickX, deathKickY);
        else rb.velocity = new Vector2(deathKickX, deathKickY);
        isAlive = false;
        OnPlayerDeath?.Invoke();
    }

    void Run()
    {
        rb.velocity = new Vector2(runSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        anim.SetBool("running", playerHasHorizontalSpeed);
    }

    void Jump()
    {
        rb.velocity += new Vector2(0f, jumpSpeed);
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
    }

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleOffLadder;
            anim.SetBool("climbing", false);
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * climbSpeed);
        rb.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        anim.SetBool("climbing", playerHasVerticalSpeed);
    }
}
