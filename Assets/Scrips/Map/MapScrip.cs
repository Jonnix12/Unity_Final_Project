using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScrip : MonoBehaviour
{
    
    public int index;
    public bool haveBoss;
    [HideInInspector] public float minX;
    [HideInInspector] public float MaxX;

    private void Start()
    {
        minX = transform.position.x - 9f;
        MaxX = transform.position.x + 9f;
    }

    public void SetMap(int index)
    {
        this.index = index;
    }

    public void SetMap(int index, bool haveBoss)
    {
        this.index = index;
        this.haveBoss = haveBoss;
    }

}
