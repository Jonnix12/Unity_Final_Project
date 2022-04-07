using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] currentMaps;
    GameObject[] mapsType1;
    GameObject[] mapsType2;
    GameObject[] mapsType3;
    GameObject endMap1;
    GameObject endMap2;
    GameObject endMap3;
    public GameObject currentEndMap;

    EnemyManager enemyManager;
    GameManager gameManager;
    //Defines 3 different lists to store all types of map situations
    List<MapScrip> activeMaps;
    Stack<MapScrip> backDisableMaps;
    Stack<MapScrip> frontDisableMaps;


    [SerializeField] public int numOfMaps;
    [SerializeField] int numOfEnemyPerMap;
    [SerializeField] int mapType;
    [SerializeField] bool haveBoss;
    int index = 0;
    public int currentMapIndex = 0;


    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        gameManager = FindObjectOfType<GameManager>();

        mapsType1 = Resources.LoadAll<GameObject>("Maps/MapType1");
        mapsType2 = Resources.LoadAll<GameObject>("Maps/MapType2");
        mapsType3 = Resources.LoadAll<GameObject>("Maps/MapType3");
        endMap1 = Resources.Load<GameObject>("Maps/EndMaps/EndMap1/EndMap1");
        endMap2 = Resources.Load<GameObject>("Maps/EndMaps/EndMap2/EndMap2");
        endMap3 = Resources.Load<GameObject>("Maps/EndMaps/EndMap3/EndMap3");
    }

    void Start()//Creates all the maps at the beginning of the scene
    {
        backDisableMaps = new Stack<MapScrip>();
        activeMaps = new List<MapScrip>();
        frontDisableMaps = new Stack<MapScrip>();

        if (mapType == 1)
        {
            SoundManager.PlaySound(SoundManager.MapType1);
            currentMaps = mapsType1;
            currentEndMap = endMap1;
        }
        else if (mapType == 2)
        {
            SoundManager.PlaySound(SoundManager.MapType2);
            currentMaps = mapsType2;
            currentEndMap = endMap2;
        }
        else if (mapType == 3)
        {
            SoundManager.PlaySound(SoundManager.MapType3);
            currentMaps = mapsType3;
            currentEndMap = endMap3;
        }


        for (int i = 0; i < numOfMaps; i++)
        {
            SpawnMap(currentMaps);
        }
        SpawnEndMap(currentEndMap);
        MapSetup();

        
        gameManager.ActivePlayer();

    }


    public void UpdateMaps()//A function called as soon as a player moves to another map and updates the map
    {
        //Calls on all relevant functions to check if something needs to be changed
        StoreBackMap();
        ReActiveFrontMap();
        ReActiveBackMap();
        StoreFrontMaps();
    }


    public void UpdateMapIndex(int CurrentIndex)//Receives from the player the index of the map on which the player stands
    {
        currentMapIndex = CurrentIndex;
    }

    //I check the distance of the map using the index of the map if the index is 2 times smaller from the index of the current map That the player stands on then I store it and turn it off. 
    //The same goes for if the index is 2 times larger than the current index I store the map and turn it off.
    //So there are always 4 active maps 2 from the front and 2 from the back of the player.

    void StoreBackMap()//Checks if a map is too far back and if so turns it off and puts it on the list of relevant maps
    {
        MapScrip tempMap = activeMaps[0];

        if (tempMap.index < currentMapIndex - 2)
        {
            activeMaps.Remove(tempMap);
            backDisableMaps.Push(tempMap);
            tempMap.gameObject.SetActive(false);
        }
    }

    void StoreFrontMaps()
    {
        MapScrip tempMap = activeMaps[activeMaps.Count - 1];

        if (tempMap.index > currentMapIndex + 2)
        {
            activeMaps.Remove(tempMap);
            frontDisableMaps.Push(tempMap);
            tempMap.gameObject.SetActive(false);
        }
    }

    void ReActiveBackMap()//Returns inactive maps
    {
        if (backDisableMaps.Count == 0)
            return;

        if (backDisableMaps.Peek().index > currentMapIndex - 2)
        {
            MapScrip temp;
            temp = backDisableMaps.Pop();
            activeMaps.Insert(0, temp);
            temp.gameObject.SetActive(true);
        }

    }

    void ReActiveFrontMap()
    {
        if (frontDisableMaps.Count == 0)
            return;

        if (frontDisableMaps.Peek().index < currentMapIndex + 2)
        {
            MapScrip temp;
            temp = frontDisableMaps.Pop();
            activeMaps.Insert(activeMaps.Count, temp);
            temp.gameObject.SetActive(true);
        }

    }

    //Spawns the maps to the world, to every map I also Spawns enemies.
    //The spawning location is basically the width of the map which is 18 times the index of the map.
    void SpawnMap(GameObject[] maps)
    {
        int SpawnPositionX = 18 * index;
        MapScrip temp;

        temp = Instantiate(maps[Random.Range(0, maps.Length)], new Vector2(SpawnPositionX, 0), new Quaternion(0, 0, 0, 0), this.transform).GetComponent<MapScrip>();
        temp.SetMap(index);

        activeMaps.Add(temp);
        enemyManager.SpawnEnemy(temp, numOfEnemyPerMap);
        index++;
    }
    void SpawnEndMap(GameObject map)
    {
        int SpawnPositionX = 18 * index;
        MapScrip temp;

        temp = Instantiate(map, new Vector2(SpawnPositionX, 0), new Quaternion(0, 0, 0, 0), this.transform).GetComponent<MapScrip>();
        temp.SetMap(index,haveBoss);

        if (haveBoss)
        {
            enemyManager.SpawnBoss(temp);
        }
        activeMaps.Add(temp);

        index++;
    }

    public void MapSetup()//Performs a check on all the maps I have inserted into the world and verifies which of them should be turned off at the beginning of the lavel
    {
        for (int i = activeMaps.Count - 1; i > 2; i--)
        {
            frontDisableMaps.Push(activeMaps[i]);
            activeMaps[i].gameObject.SetActive(false);
            activeMaps.Remove(activeMaps[i]);
        }
    }
}
