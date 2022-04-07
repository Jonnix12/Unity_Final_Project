using UnityEngine;
using System.IO;
//The following using is to make my code binary. I found it on the internet
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerLoadSave playerData)
    {
        //The decision to make the file binery came from understanding how difficult is for hackers to hack this type of code.
        BinaryFormatter formatter = new BinaryFormatter();

        //I could write the path "C:/System/" bla bla bla
        //Unity has the meth  that locate where the game is installed 
        //Because its binary i can make it any data type i want. I decided to make it text
        //So this is the path where the file is gonna be saved
        string path = Application.persistentDataPath + "/player.text";

        //This this how i create the file
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.text";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

}
