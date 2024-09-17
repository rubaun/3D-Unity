using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private GameObject[] moedas;

    [Header("Imagens da Pontuação")]
    [SerializeField] private Image status;
    [SerializeField] private Sprite zero;
    [SerializeField] private Sprite sorriso;
    [SerializeField] private Sprite alegria;
    [SerializeField] private Sprite oculos;

    //Avisos
    [Header("Aviso Missão")]
    [SerializeField] private TextMeshProUGUI avisoMissao;
    [SerializeField] private AudioClip somMissao;
    [SerializeField] private GameObject luzSaida;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        pontos = GameObject.Find("Pontos").GetComponent<TextMeshProUGUI>();
        pontosRestantes = GameObject.Find("PontosRestantes").GetComponent<TextMeshProUGUI>();
        moedas = GameObject.FindGameObjectsWithTag("Moeda");
        pontosRestantes.text = moedas.Length.ToString();
        //----
        status = GameObject.Find("Status").GetComponent<Image>();
        audioPlayer = GetComponent<AudioSource>();
        telaMorte.SetActive(false);
        status.sprite = zero;
        //----
        avisoMissao = GameObject.Find("Aviso").GetComponent<TextMeshProUGUI>();
        luzSaida.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
            avisoMissao.text = "Saída Liberada";
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
    
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
