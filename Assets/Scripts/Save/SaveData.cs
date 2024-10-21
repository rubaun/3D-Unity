using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public float playerX;
    public float playerY;
    public float playerZ;
    public int pontos;
    public int cenaAtual;

    public SaveData(Player player)
    {
        player.playerX = playerX;
        player.playerY = playerY;
        player.playerZ = playerZ;
        player.pontos = pontos;
        player.cenaAtual = cenaAtual;
    }
}
