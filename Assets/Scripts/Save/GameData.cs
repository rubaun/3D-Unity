

[System.Serializable]
public class GameData
{
    public float[] position;
    public int pontosPlayer;
    public int cenaAtualPlayer;

    public GameData(PlayerMove player)
    {
        position = new float[3];
        position[0] = player.GetPosicaoPlayer().x;
        position[1] = player.GetPosicaoPlayer().y;
        position[2] = player.GetPosicaoPlayer().z;
        pontosPlayer = player.GetPontos();
        cenaAtualPlayer = player.GetCenaAtual();
    }
}
