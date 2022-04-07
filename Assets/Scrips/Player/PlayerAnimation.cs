using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerManager manager;

    Animator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //Performs regular animations Check(run and if Grounded)
        animator.SetFloat("Velocity.Y", rb.velocity.y);

        if (rb.velocity.x > 0.1 || rb.velocity.x < -0.1)
            animator.SetBool("IsRuning", true);
        else
            animator.SetBool("IsRuning", false);
        if (manager.movement.IsGrounded())
            animator.SetBool("IsGrounded", true);
        else
            animator.SetBool("IsGrounded", false);

    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void HurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    public void DeadAnimation()
    {
        animator.SetBool("IsDead", true);
    }
}
