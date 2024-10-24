using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Diretor : MonoBehaviour
{ 
    //Player
    [SerializeField] private PlayerMove player;
    //Tela da Morte
    [SerializeField] private GameObject telaMorte;
    //Audio Source
    private AudioSource audioPlayer;
    //Som de morte
    [SerializeField] private AudioClip morte;

    //Texto Pontos
    [SerializeField] private TextMeshProUGUI pontos;
    [SerializeField] private TextMeshProUGUI pontosRestantes;
    [SerializeField] private GameObject[] moedas;

    [Header("Imagens da Pontua��o")]
    [SerializeField] private Image status;
    [SerializeField] private Sprite zero;
    [SerializeField] private Sprite sorriso;
    [SerializeField] private Sprite alegria;
    [SerializeField] private Sprite oculos;

    //Avisos
    [Header("Aviso Miss�o")]
    [SerializeField] private TextMeshProUGUI avisoMissao;
    [SerializeField] private AudioClip somMissao;
    [SerializeField] private GameObject luzSaida;

    //Controle de Fases
    [Header("Fases e Telas")]
    [SerializeField] private SceneAsset[] scenes;
    [SerializeField] private int cenaAtual;

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ProcurarReferencias();
    }

    // Update is called once per frame
    void Update()
    {
        if(telaMorte == null)
        {
            ProcurarReferencias();
        }

        int contagem = player.ContagemPontos();

        if (!player.VerificaSePlayerEstaVivo())
        {
            audioPlayer.PlayOneShot(morte);
            telaMorte.SetActive(true);
        }

        pontos.text = contagem.ToString();

        switch (contagem)
        {
            case 0:
                status.sprite = zero;
                break;
            case 1:
                status.sprite = sorriso;
                break;
            case 5:
                status.sprite = alegria;
                break;
            case 10:
                status.sprite = oculos;
                break;
        }

        if(player.ContagemPontos() >= moedas.Length)
        {
            luzSaida.SetActive(true);
            avisoMissao.text = "Sa�da Liberada";
            audioPlayer.PlayOneShot(somMissao);
        }
        else
        {
            avisoMissao.text = "Colete as moedas";
        }
    }

    public bool PodePassarFase()
    {
        if(player.ContagemPontos() >= moedas.Length)
        {
            return true;
        }

        return false;
    }

    public void ProximaFase()
    {
        if(cenaAtual < scenes.Length)
        {
            SceneManager.LoadScene(scenes[cenaAtual + 1].name);
            cenaAtual++;
        }
        else
        {
            SceneManager.LoadScene("Final");
        }
        
    }

    private void ProcurarReferencias()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        status = GameObject.FindWithTag("Status").GetComponent<Image>();
        telaMorte = GameObject.FindWithTag("TelaDaMorte");
        pontos = GameObject.FindWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        pontosRestantes = GameObject.FindWithTag("PontosRestantes").GetComponent<TextMeshProUGUI>();
        moedas = GameObject.FindGameObjectsWithTag("Moeda");
        avisoMissao = GameObject.FindWithTag("Aviso").GetComponent<TextMeshProUGUI>();
        luzSaida = GameObject.FindWithTag("LuzSaida");
        audioPlayer = GetComponent<AudioSource>();
        telaMorte.SetActive(false);
        status.sprite = zero;
        pontosRestantes.text = moedas.Length.ToString();
    }

    public int GetCenaAtual()
    {
        return cenaAtual;
    }

    public void SavePlayer(PlayerMove player)
    {
        SaveSystem.Save(player);
    }

    public void LoadPlayer()
    {
        GameData data = SaveSystem.Load();

        SceneManager.LoadScene(data.cenaAtualPlayer);

        player.SetPontos(data.pontosPlayer);
        player.SetPosicaoPlayer(new Vector3(data.position[0], data.position[1], data.position[2]));
    }
}
