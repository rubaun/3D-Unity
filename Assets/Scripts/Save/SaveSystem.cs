using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    
    public static void Save(PlayerMove player)
    {
        BinaryFormatter formater = new BinaryFormatter();//Cria um formatador binário
        string path = Application.persistentDataPath + "/player.adventure"; //Define a localização e o nome do arquivo player.adventure
        FileStream stream = new FileStream(path, FileMode.Create); //Cria o arquivo no local especificado

        GameData data = new GameData(player); //Cria um objeto SaveData com os dados do player

        formater.Serialize(stream, data); //Serializa os dados do player no arquivo
        stream.Close(); //Fecha o arquivo
    }

    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/player.adventure"; //Define a localização e o nome do arquivo player.adventure

        if (File.Exists(path)) //Se o arquivo existir
        {
            BinaryFormatter formater = new BinaryFormatter(); //Cria um formatador binário
            FileStream stream = new FileStream(path, FileMode.Open); //Abre o arquivo

            GameData data = formater.Deserialize(stream) as GameData; //Deserializa os dados do arquivo
            stream.Close(); //Fecha o arquivo

            return data; //Retorna um objeto PlayerMove com os dados do arquivo
        }
        else
        {
            Debug.LogError("Save file not found in " + path); //Se o arquivo não existir, exibe uma mensagem de erro
            return null; //Retorna nulo
        }
    }
}
