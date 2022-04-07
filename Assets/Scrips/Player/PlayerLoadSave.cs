using UnityEngine;

public class PlayerLoadSave : MonoBehaviour
{
    PlayerManager playerManager;
    MapManager mapManager;

    [HideInInspector] public int saveCurrentLives;
    [HideInInspector] public int SaveMaxHealth;
    [HideInInspector] public float saveCurrentHealth;
    [HideInInspector] public int saveMapIndex;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        mapManager = GameObject.FindGameObjectWithTag("Level").GetComponent<MapManager>();
    }


    public void GetValus()
    {
        saveCurrentLives = playerManager.combet.currentLives;
        saveCurrentHealth = playerManager.combet.currentHealth;
        SaveMaxHealth = PlayerCombet.maxHealth;
        saveMapIndex = mapManager.currentMapIndex;
        Debug.Log(saveMapIndex);
    }

    public void SavePlayer()
    {
        GetValus();
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        int checkHowMuchToLoad = mapManager.currentMapIndex - saveMapIndex;
        mapManager.currentMapIndex = saveMapIndex;
        Debug.Log(mapManager.currentMapIndex);
        for (int i = 0; i < checkHowMuchToLoad; i++)
        {
            mapManager.UpdateMaps();
        }
        Debug.Log(mapManager.currentMapIndex);

        PlayerData data = SaveSystem.LoadPlayer();
        PlayerCombet.maxHealth = data.maxHealth;
        playerManager.combet.currentHealth = data.currentHealth;
        playerManager.combet.currentLives = data.currentLives;


        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        playerManager.uI.healthBar.SetHealth(saveCurrentHealth);

        Time.timeScale = 1f; //Normal time
        PauseMenu.GameIsPause = false;
    }
}
