using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    //Creates a reference for all the relevant scripts
    [HideInInspector] public playerMovement movement;
    [HideInInspector] public PlayerCombet combet;
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public PlayerUI uI;
    [HideInInspector] public PlayerLoadSave loadSave;
    [HideInInspector] public PlayerHandel playerHandel;

    GameManager gameManager;

    static public int Coins;
    

    private void Awake()
    {
        uI = GetComponent<PlayerUI>();
        movement = GetComponent<playerMovement>();
        combet = GetComponent<PlayerCombet>();
        playerAnimation = GetComponent<PlayerAnimation>();
        loadSave = GetComponent<PlayerLoadSave>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.GetPlayerRef(gameObject);
    }

    void Update()
    {
        if (!combet.IsDead())//check if the player is not dead
        {
            if (movement.canMove)//let the player move
            {
                movement.Movement();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                combet.Attack();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                combet.ShotFireBall();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                combet.UsePotion();
            }

            if (transform.position.y< -50)
            {
                combet.currentLives--;
                transform.position = new Vector2(transform.position.x-5, 0);
                combet.currentHealth = PlayerCombet.maxHealth;
            }
            Debug.Log(transform.position.y);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Inter"))
        {
            SoundManager.PlaySound(SoundManager.Coin);
            int HowMuchCoinsToAdd = Random.Range(1, 4);
            Coins += HowMuchCoinsToAdd;
            Interactable interactable = new Interactable();
            interactable.AactiveInteractable(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("EndMap"))
        {
           
            gameObject.SetActive(false);
            transform.position = new Vector2(0, 0);
            

            if (collision.GetComponent<MapScrip>().haveBoss)
                gameManager.LoadMarket();
            else
                gameManager.NextScene();
        }
    }

}
