using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    EnemyAI enemyAI;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Vector2 direction;
    Vector2 lestCheckPoint;


    [SerializeField] int speed = 3;
    [SerializeField] float jumpMuliteplayer = 6f;
    [SerializeField] float stuckJumpMuliteplayer = 10f;
    [SerializeField] float checkPointOfSet = 0.2f;
    [SerializeField] LayerMask layer;

    [HideInInspector] public bool faceingRight = true;
    public int stuckCount = 3;
    int _stuckCount;
    

    readonly int startWaitStackTime = 5;
    readonly int startWaitCheckPointTime = 3;
    float waitStackTime;
    float waitCheckPointTime;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        waitStackTime = startWaitStackTime;
        _stuckCount = stuckCount;
    }

    public bool Stuck()//A function that drop a point on the map every second in the position of the AI, if the AI distance is smaller than the set distance the function will activate a timer, as soon as the timer runs out and the AI still can not move it will set itself stuck
    {
        DropCheckPoint();

        float distanceTolestCheckPoint;
        distanceTolestCheckPoint = Vector2.Distance(lestCheckPoint, transform.position);

        if (distanceTolestCheckPoint < checkPointOfSet && !enemyAI.patrol.rachTarget)
        {
            if (waitStackTime >= 0)
            {
                waitStackTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Stock");
                waitStackTime = startWaitStackTime;
                return true;
            }
        }
        else
            waitStackTime = startWaitStackTime;

        return false;

    }

    public bool StuckCount()//A function that counts to 3, and returns true when it finishes and resets itself
    {
        if (_stuckCount > 0)
        {
            _stuckCount--;
            return true;
        }
        else 
        {
            _stuckCount = stuckCount;
            return false;
        }
    }

    void DropCheckPoint()//The function that drops a point every second in the position of the AI
    {
        if (waitCheckPointTime >= 0)
        {
            waitCheckPointTime -= Time.deltaTime;
        }
        else
        {
            lestCheckPoint = transform.position;
            waitCheckPointTime = startWaitCheckPointTime;
        }
    }

    public bool MoveToComplete(Vector2 Point, float distanceToPoint)//A function that returns true when the AI reached the point, And moves the AI to the point when he is grounded and flip when needed
    {
        if (IsGrounded())
        {
            direction = (Point - (Vector2)transform.position).normalized;
            Flip(Point);
            if (Vector2.Distance(transform.position, Point) > distanceToPoint)
            {
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
                enemyAI.enemyAnimation.SetBool("isWalk", true);
            }
            else return true;

            return false;
        }

        return false;
    }

    public void Jump()//jump....
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpMuliteplayer);
        }
    }

    public void StuckJump()//A more powerful jump, with some correction to help the AI overcome the obstacle
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (faceingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xPos - 0.5f, yPos), 0);
            if (IsGrounded())
                rb.velocity = new Vector2(3, stuckJumpMuliteplayer);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xPos + 0.5f, yPos), 0);
            if (IsGrounded())
                rb.velocity = new Vector2(-3, stuckJumpMuliteplayer);
        }
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
        enemyAI.enemyAnimation.SetBool("isWalk", false);
        enemyAI.enemyAnimation.SetBool("isRuning", false);
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, 0.1f, layer);
        return raycastHit2D.collider != null;
    }

    void Flip(Vector2 dir)
    {
        if (dir.x < transform.position.x && faceingRight)
        {
            faceingRight = false;
            transform.Rotate(new Vector3(0, 180, 0), Space.World);
        }
        else if (dir.x > transform.position.x && !faceingRight)
        {
            faceingRight = true;
            transform.Rotate(new Vector3(0, 180, 0), Space.World);
        }
    }
   
    public void DisableGravity()//Disable gravity for the moment the AI dies so that it does not fall out of the map due to the collider being Disable
    {
        rb.gravityScale = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(lestCheckPoint, 0.2f);
    }

}
