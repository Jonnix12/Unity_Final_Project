using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombet : MonoBehaviour
{
    PlayerManager manager;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public Queue<FireBall> fireBalls;

    GameObject fireBallObj;

    public static int maxHealth = 100;
    public static int maxLifes = 3;
    static public int healthPotion = 2;
    //We have two types of life,
    //(normal life of 100 to 0, plus 3 life points, which decrease every time life reaches from 100 to 0,
    //as soon as the 3 life points reaches 0 the player dies)
    [HideInInspector] public float currentHealth;
    [HideInInspector] public int currentLives;

    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] static public float damage = 20f;
    float attackWaitTime;
    bool canAttack = true;

    float waitTime = 2;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }
    void Start()
    {
        fireBalls = new Queue<FireBall>();
        currentHealth = maxHealth;
        currentLives = maxLifes;
        attackWaitTime = attackSpeed;
        fireBallObj = Resources.Load<GameObject>("Items/FireBall/FireBall");
        LoadFireBall();
    }

    public void Attack()//An attack function that spawn a collider at a set point, if the collider picks up enemies it damage them
    {
        if (canAttack)
        {
            manager.playerAnimation.AttackAnimation();

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<AiCombat>().Hurt(damage);
            }

            canAttack = false;
        }
    }
    private void OnEnable()
    {
        LoadFireBall();
    }

    private void Update()//A timer that runs all the time if the player is valid to attack (create a daily for the attack)
    {
        if (!canAttack)
        {
            if (attackWaitTime >= 0)
            {
                attackWaitTime -= Time.deltaTime;
            }
            else
            {
                attackWaitTime = attackSpeed;
                canAttack = true;
            }
        }
    }

    public void Hurt(float damage)
    {
        manager.playerAnimation.HurtAnimation();
        currentHealth -= damage;
        ReduceLive();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void ReduceLive()
    {
        if (currentHealth <= 0)
        {
            currentLives--;
            currentHealth = maxHealth;
        }
    }

    public bool IsDead()
    {
        if (currentLives < 0)
        {
            manager.playerAnimation.DeadAnimation();
            LoadGameOver();
            return true;
        }

        return false;
    }

    public void LoadGameOver()
    {
        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("PlayerGroup"));
            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
    }

    void LoadFireBall()
    {
        if (fireBallObj != null)
        {
            for (int i = 0; i < 5; i++)
            {
                FireBall temp;
                temp = Instantiate(fireBallObj, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<FireBall>();
                fireBalls.Enqueue(temp);
                temp.gameObject.SetActive(false);
            }
        }
    }

    public void ShotFireBall()
    {
        if (fireBalls.Count != 0)
        {
            FireBall temp = fireBalls.Dequeue();
            temp.transform.position = transform.position;
            if (manager.movement.faceingRight)
            {
                temp.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                temp.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            temp.gameObject.SetActive(true);
            temp.Shot();
        }
    }

    public void RenterFireBall(FireBall fireBall)
    {
        fireBalls.Enqueue(fireBall);
        fireBall.gameObject.SetActive(false);
    }

    public void UsePotion()
    {
        if(healthPotion >0 && currentHealth<maxHealth)
        {
            if (maxHealth - currentHealth > 10)
                currentHealth += 10;
            else
                currentHealth = maxHealth;
            healthPotion--;
        }
    }
}
