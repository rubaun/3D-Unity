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
    [Header("Imagens da Pontuação")]
    [SerializeField] private Image status;
    [SerializeField] private Sprite zero;
    [SerializeField] private Sprite sorriso;
    [SerializeField] private Sprite alegria;
    [SerializeField] private Sprite oculos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        pontos = GameObject.Find("Pontos").GetComponent<TextMeshProUGUI>();
        status = GameObject.Find("Status").GetComponent<Image>();
        audioPlayer = GetComponent<AudioSource>();
        telaMorte.SetActive(false);
        status.sprite = zero;
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
