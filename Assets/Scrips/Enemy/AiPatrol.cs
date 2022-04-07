using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrol : MonoBehaviour
{
    EnemyAI enemyAI;

    [HideInInspector] public bool rachTarget = false;
    [SerializeField] float distanceToPoint = 0.2f;
    [SerializeField] int startWaitTime = 3;
    [SerializeField] LayerMask layer;

    float waitTime;

    int pointA;
    int pointB;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        waitTime = startWaitTime;
    }

    public bool MoveToPoint(Vector2 target)//A function that returns true when it has reached the point and waited for the required time
    {
        if (enemyAI.movement.MoveToComplete(target, distanceToPoint))
        {
            if (waitTime >= 0)//Timer function
            {
                enemyAI.movement.Stop();
                waitTime -= Time.deltaTime;
            }
            else
            {
                waitTime = startWaitTime;
                rachTarget = true;
                return true;
            }
        }

        rachTarget = false;
        return false;
    }

    public Vector2 GetTarget() //A function that gives a point on the map that the AI can go to
    {
        Vector2 temp;
        Vector2 targetRayCastOrigin;

        int pointX = Random.Range(pointA, pointB);
        targetRayCastOrigin = new Vector2(pointX, 6f);
        RaycastHit2D raycast = Physics2D.Raycast(targetRayCastOrigin, Vector2.down, 20f, layer);

        temp = raycast.point;
        temp += new Vector2(0.5f, 0.5f);
        return temp;
    }

    public void SetBounds(float minX, float MaxX)//A function that receives from the map that the AI stand on the boundaries of the map
    {
        pointA = (int)minX;
        pointB = (int)MaxX;
    }
}
