using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //A script that keeps all the enemies on the list
    //And spawns enemies in the relevant map and the relevant quantity
    public List<GameObject> enemies;
    GameObject[] loadEnemes;
    GameObject boss;
    Vector2 spawnPosition;
    
    private void Awake()
    {
        enemies = new List<GameObject>();
        loadEnemes = Resources.LoadAll<GameObject>("Enemy/Enemys");
        boss = Resources.Load<GameObject>("Enemy/Boss/Boss");
    }

    public void SpawnEnemy(MapScrip map, int numOfEnemy)
    {
        EnemyAI temp;
        //Determines the boundaries of the patrol function of the AI, according to the boundaries of the map on which it is located
        int X = 18 * map.index;
        float minX = X - 8.5f;
        float maxX = X + 8.5f;
        //Randomly determines which enemy to spawn
        int spawnIndex = Random.Range(0, loadEnemes.Length);
        //Spawns enemies in a pre-defined amount within the boundaries of the map
        for (int i = 0; i < numOfEnemy; i++)
        {
            spawnPosition.x = Random.Range(minX, maxX);
            spawnPosition.y = -2f;

            temp = Instantiate(loadEnemes[spawnIndex], spawnPosition, new Quaternion(0f, 0f, 0f, 0f), map.transform).GetComponent<EnemyAI>();
            temp.patrol.SetBounds(minX, maxX);
            enemies.Add(temp.gameObject);
        }
    }

    public void SpawnBoss(MapScrip map)
    {
        EnemyAI temp;
        //Determines the boundaries of the patrol function of the AI, according to the boundaries of the map on which it is located
        int X = 18 * map.index;
        float minX = X - 8.5f;
        float maxX = X + 8.5f;

        spawnPosition.x = Random.Range(minX, maxX);
        spawnPosition.y = -2f;

        temp = Instantiate(boss, spawnPosition, new Quaternion(0f, 0f, 0f, 0f), map.transform).GetComponent<EnemyAI>();
        temp.patrol.SetBounds(minX, maxX);
        enemies.Add(temp.gameObject);
    }
}
