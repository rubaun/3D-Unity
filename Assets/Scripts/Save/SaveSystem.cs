using UnityEngine;
using System.IO;


public static class SaveSystem
{
    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.playerX = playerX;
        saveData.playerY = playerY;
        saveData.playerZ = playerZ;
        saveData.pontos = pontos;
        saveData.pontosRestantes = pontosRestantes;
        saveData.scenes = scenes;
        saveData.cenaAtual = cenaAtual;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            playerX = saveData.playerX;
            playerY = saveData.playerY;
            playerZ = saveData.playerZ;
            pontos = saveData.pontos;
            pontosRestantes = saveData.pontosRestantes;
            scenes = saveData.scenes;
            cenaAtual = saveData.cenaAtual;
        }
    }
}
