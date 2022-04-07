using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData 
{
    //public int level;
    public int maxHealth;
    public float currentHealth;
    public int currentLives;
    public int mapIndex;
    public float[] position;

    public PlayerData (PlayerLoadSave playerData)
    {
        maxHealth = playerData.SaveMaxHealth;
        currentHealth = playerData.saveCurrentHealth;
        currentLives = playerData.saveCurrentLives;
        mapIndex = playerData.saveMapIndex;

        //Saving his position in map
        position = new float[3];
        position[0] = playerData.transform.position.x; //X
        position[1] = playerData.transform.position.y; //Y
        position[1] = 10; //Y
        position[2] = playerData.transform.position.z; //Z
    }

}
