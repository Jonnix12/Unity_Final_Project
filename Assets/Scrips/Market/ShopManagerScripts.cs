using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManagerScripts : MonoBehaviour
{
    public int[,] shopItems = new int[6,6]; //How many items you want in your shop
    public Text CoinsText;

    GameManager gameManager;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        CoinsText.text = "Coins:" + PlayerManager.Coins.ToString();
        //For some reason That was a problem to start from 0.
        //The right number in the [,] is the item id
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;

        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;
        shopItems[2, 5] = 50;

        //Quanity
        shopItems[3, 1] = ((int)FireBall.Damage);
        shopItems[3, 2] = PlayerCombet.maxLifes;
        shopItems[3, 3] = ((int)PlayerCombet.damage);
        shopItems[3, 4] = PlayerCombet.maxHealth;
        shopItems[3, 5] = PlayerCombet.healthPotion;

        //How much to incrase
        shopItems[4, 1] = 1;
        shopItems[4, 2] = 1;
        shopItems[4, 3] = 5;
        shopItems[4, 4] = 10;
        shopItems[4, 5] = 1;
        Debug.Log(PlayerManager.Coins);
    }

    public void Buy()
    {
        //Adding reference to the button to the event system in unity
        //By addint the ref I can check which button i pressed 
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (PlayerManager.Coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            PlayerManager.Coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];

            if (shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 1) //FireBall
                FireBall.Damage += 1;

            else if (shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 2) //Extra Life
                PlayerCombet.maxLifes += 1;

            else if (shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 3) //Incrase hit power/damage
                PlayerCombet.damage += 5;

            else if (shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 4) //Incrase Max health
                PlayerCombet.maxHealth += 10;

            else if (shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 5) //Health potion
                PlayerCombet.healthPotion += 1;

        shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID] += shopItems[4, ButtonRef.GetComponent<ButtonInfo>().ItemID];
        CoinsText.text = "Coins:" + PlayerManager.Coins.ToString();
        ButtonRef.GetComponent<ButtonInfo>().QuantityText.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
    }

    public void Exit()
    {
        gameManager.NextScene();
    }
}
