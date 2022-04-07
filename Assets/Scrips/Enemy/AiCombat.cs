using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombat : MonoBehaviour
{
    EnemyAI enemyAI;
    PlayerCombet target;
    Collider2D enemyCollider;

    [SerializeField] Transform attackPoint;
    [SerializeField] public float health = 100;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float damage = 5f;
    [SerializeField] float attackSpeed = 1f;
    LayerMask layer;

    bool addCoinOnce = false;

    float attackWaitTime;

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        layer = LayerMask.GetMask("Player");
        enemyCollider = GetComponent<CapsuleCollider2D>();
        attackWaitTime = attackSpeed;
    }

    public void CombatSetUp(Transform target)//Finds the player combat script
    {
        if (target != null)
        {
            this.target = target.GetComponent<PlayerCombet>();
        }
    }

    public void Attack()//Attack function with Daily, also activates a random animation for the attack
    {
        if (attackWaitTime >= 0)
        {
            attackWaitTime -= Time.deltaTime;
        }
        else
        {
            if (enemyAI.movement.MoveToComplete(target.transform.position, 1.5f))//Maintains range of attack
            {
                int attack = Random.Range(1, 3);//Selects random attack animation
                if (attack == 1)
                    enemyAI.enemyAnimation.SetTrigger("Attack1");
                else
                    enemyAI.enemyAnimation.SetTrigger("Attack2");

                Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, attackRange, layer);
                if (collider != null)
                {
                    target.Hurt(damage);//Hits the player
                }

                attackWaitTime = attackSpeed;
            }
        }
    }

    public bool InRange()//Checks if the AI is still within attack range
    {
        if (Vector2.Distance(transform.position, target.transform.position) < 2)
        {
            return true;
        }
        return false;
    }

    public void Hurt(float damage)
    {
        enemyAI.enemyAnimation.SetTrigger("Hit");
        health -= damage;
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            
            enemyAI.enemyManager.enemies.Remove(gameObject);

            enemyAI.movement.Stop();
            enemyAI.movement.DisableGravity();
            enemyCollider.enabled = false;

            enemyAI.enemyAnimation.SetBool("isDead", true);
            Destroy(gameObject, 5f);
            if (addCoinOnce == true)
                return true;
            else
            {
                Debug.Log("Enter coin");
                int howMuchToAdd = Random.Range(1, 7);
            PlayerManager.Coins +=howMuchToAdd;
                addCoinOnce = true;
            }
            return true;
        }
        return false;
    }
}
