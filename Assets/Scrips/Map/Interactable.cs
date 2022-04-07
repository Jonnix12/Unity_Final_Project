using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable
{
    public GameObject box;
    public GameObject coin;

    public void AactiveInteractable(GameObject interactable)
    {
        //Instantiate(coin, interactable.transform);
        //coin.transform.position.x = coin.transform.position.x + 1;
        interactable.SetActive(false);
    }
}
