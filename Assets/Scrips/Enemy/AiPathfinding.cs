using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPathfinding : MonoBehaviour
{
    EnemyAI enemyAI;

    [HideInInspector] public bool seeTarget = false;//A variable that determines if the AI sees the player, changes it in OnTriggerEnter and OnTriggerExit
    [SerializeField] int SearchTime = 2;
    [SerializeField] int maxSearchTime = 10;
    [SerializeField] float entarCombatRange = 2f;
    float waitTime;
    float maxWaitTime;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    void Start()
    {
        waitTime = SearchTime;
        maxWaitTime = maxSearchTime;
    }

  
    public bool MoveToTarget(Vector2 target)//Move towards the target
    {
        if (seeTarget && enemyAI.movement.MoveToComplete(target, entarCombatRange))
        {
            return true;
        }

        return false;
    }


    public bool SearchTarget(Vector2 target)//Move to the last point he saw the player and look for him for a set time
    {
        if (enemyAI.movement.MoveToComplete(target,0.5f))
        {
            if (waitTime >= 0)
            {

                waitTime -= Time.deltaTime;
            }
            else
            {
                waitTime = SearchTime;
                return true;
            }
       
        }
        else//Timer that if the AI fails to reach the last point he saw the player then he gives up (Maximum time for the search)
        {
            if (maxWaitTime >= 0)
            {

                maxWaitTime -= Time.deltaTime;
            }
            else
            {
                maxWaitTime = maxSearchTime;
                return true;
            }
        }

        return false;
    }
}
