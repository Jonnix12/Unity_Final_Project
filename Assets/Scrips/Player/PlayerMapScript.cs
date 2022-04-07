using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapScript : MonoBehaviour
{
    [SerializeField] int mapIndex;
    
    MapManager mapManager;

    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Each time the player moves a map he updates the current index and tells the map manager to update the maps
        if (collision.gameObject.tag.Contains("MapPart"))
        {
            mapIndex = collision.gameObject.GetComponentInParent<MapScrip>().index;
            mapManager.UpdateMapIndex(mapIndex);

            mapManager.UpdateMaps();
        }
    }

    private void OnDisable()
    {
        mapIndex = 0;
    }

    private void OnEnable()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
}
