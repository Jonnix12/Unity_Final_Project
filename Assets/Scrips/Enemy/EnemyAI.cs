using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Creates a reference for all the relevant scripts
    [HideInInspector] public AiPathfinding pathfinding;
    [HideInInspector] public AiPatrol patrol;
    [HideInInspector] public AiCombat combat;
    [HideInInspector] public AiMovement movement;
    [HideInInspector] public EnemyAnimation enemyAnimation;
    [HideInInspector] public EnemyManager enemyManager;
    //Variables for state machine
    public string currentState;
    string[] state;
    //Variable target and direction of movement
    Transform target;
    Vector2 movePoint;

    private void Awake()
    {
        patrol = GetComponent<AiPatrol>();
        pathfinding = GetComponent<AiPathfinding>();
        combat = GetComponent<AiCombat>();
        movement = GetComponent<AiMovement>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyManager = FindObjectOfType<EnemyManager>();

        state = new string[] { "Patrol", "Chase", "Combat" };
    }


    private void OnEnable()
    {
        SetToPatrolState();//OnEnable puts it in patrol mode
    }

    void Update()
    {
        if (!combat.IsDead())//First check if he's alive
        {
            switch (currentState)//Switch for the state
            {
                case "Patrol"://The logic of a patrol state
                    if (!movement.Stuck())//Check if the AI is stuck
                    {
                        if (patrol.MoveToPoint(movePoint))//Move to the point and as soon as he arrives he is looking for a new point
                        {
                            movePoint = patrol.GetTarget();//Finds a new point on the map to go to
                        }
                    }
                    else
                    {
                        if (movement.StuckCount())//If the AI gets stuck this function counts 3 times in these 3 times he tries to exit the stuck situation if he fails he will look for another place to go                      
                            movement.StuckJump();
                        else
                            movePoint = patrol.GetTarget();//Finds a new point on the map to go to
                    }
                    break;
                case "Chase"://The logic of a Chase state
                    if (!movement.Stuck())//Check if the AI is stuck
                    {
                        if (pathfinding.seeTarget)
                        {
                            if (pathfinding.MoveToTarget(target.position))//Once he reaches the target he goes into combat state
                            {
                                SetToCombatState();
                            }
                        }
                        else
                        {
                            if (pathfinding.SearchTarget(movePoint) && !pathfinding.seeTarget)
                            {
                                SetToPatrolState();
                            }
                        }
                    }
                    else
                    {
                        if (movement.StuckCount())//If the AI gets stuck this function counts 3 times in these 3 times it tries to exit if it fails it will return to patrol state
                            movement.StuckJump();
                        else
                            SetToPatrolState();
                    }
                    break;
                case "Combat"://The logic of a Combat state
                    combat.CombatSetUp(target);
                    movement.Stop();
                    if (combat.InRange())
                    {
                        combat.Attack();
                    }
                    else
                    {
                        SetToChaseState();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))//Goes into chase state and sets the player as a target
        {
            target = collision.transform;
            SetToChaseState();
            pathfinding.seeTarget = true;
        }
        if (collision.gameObject.tag.Contains("MapPart"))//the AI has a small collider that is a trigger that checks if it has reached the step and needs to jump
        {
            movement.Jump();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            movePoint = collision.gameObject.transform.position;//Defines the last position that the AI saw the player as a point to move to
            target = null;
            pathfinding.seeTarget = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Each map transition defines the new boundaries 
        //(Map change can only happen if the AI chases the player to another map, the AI can not move alone to a different map because in patrol mode it is set to stay on same map by the defined boundaries)
        if (collision.gameObject.tag.Contains("MapPart"))
        {
            gameObject.transform.parent = collision.transform;
            MapScrip temp = collision.gameObject.GetComponentInParent<MapScrip>();
            patrol.SetBounds(temp.minX + 0.5f, temp.MaxX -0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.position, 0.5f);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(movePoint, 0.5f);
    }

    public void SetToChaseState()
    {
        currentState = state[1];
    }

    public void SetToPatrolState()
    {
        movePoint = patrol.GetTarget();
        currentState = state[0];
    }

    public void SetToCombatState()
    {
        currentState = state[2];
    }
}
