using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //This Movement script is very short and simple
    PlayerManager manager;

    [SerializeField] LayerMask layer;

    [SerializeField] float speed = 10f;
    [SerializeField] float jumpMuliteplayer = 25f;

    [HideInInspector] public bool canMove = true;
    float horizontal;
    public bool faceingRight = true;

    Rigidbody2D rb;

    BoxCollider2D Collider;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
    }

    public bool IsGrounded()//Check if the player is grounded
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0f, Vector2.down, 0.1f, layer);
        return raycastHit2D.collider != null;
    }

    private void Filp()//flip the player 
    {
        transform.Rotate(new Vector3(0, 180, 0), Space.World);
    }

    public void Movement()//the movement function
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal < 0 && faceingRight)
        {
            Filp();
            faceingRight = false;
        }
        else if (horizontal > 0 && !faceingRight)
        {
            Filp();
            faceingRight = true;
        }


        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlaySound(SoundManager.PlayerJump);
            rb.velocity = Vector2.up * jumpMuliteplayer;
            manager.playerAnimation.Jump();
        }


        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
