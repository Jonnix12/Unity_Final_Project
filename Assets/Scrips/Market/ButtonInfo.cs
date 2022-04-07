using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;
    public Text QuantityText;
    public GameObject ShopManager;

    void Update()
    {
        PriceText.text = $"Price: " + ShopManager.GetComponent<ShopManagerScripts>().shopItems[2,ItemID].ToString() + " Coins";
        QuantityText.text = ShopManager.GetComponent<ShopManagerScripts>().shopItems[3, ItemID].ToString();
    }
}
