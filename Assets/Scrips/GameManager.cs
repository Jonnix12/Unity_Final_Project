using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public EnemyManager enemyManager;
    
    [HideInInspector] public SoundManager soundManager;

    public int sceneIndex = 0;
    GameObject player;

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
      
        soundManager = FindObjectOfType<SoundManager>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    public void NextScene()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
        
    }

    public void LoadMarket()
    {
        SceneManager.LoadScene(7);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GetPlayerRef(GameObject player)
    {
        this.player = player;
    }

    public void ActivePlayer()
    {
        if (player != null)
        {
            player.SetActive(true);
        }
    }
}
