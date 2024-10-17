using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BolinhaMove : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    [SerializeField] private bool invertH;
    [SerializeField] private bool invertV;
    [SerializeField] private int pontos;
    private bool estaVivo;

    [Header("Sons da Bolinha")]
    [SerializeField] private AudioClip pulo;
    [SerializeField] private AudioClip pegaCubo;
    private AudioSource audioPlayer;
    private TextMeshProUGUI textoPontos;
    private TextMeshProUGUI textoTotal;

    [Header("HUD")]
    [SerializeField] private List<Sprite> emojis = new List<Sprite>();
    [SerializeField] private GameObject telaGameOver;

    // Start is called before the first frame update
    void Start()
    { 
        estaVivo = true;
        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        textoPontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("TotalCubos").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("CuboBrilhante").Length.ToString();
        telaGameOver = GameObject.Find("GameOver");
        telaGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(estaVivo)
        {
            //Movimento
            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");

            transform.position += new Vector3(
                                        moveV * velocidade * Time.deltaTime,
                                        0,
                                        -1 * moveH * velocidade * Time.deltaTime
                                        );

            //Pulo
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
                audioPlayer.PlayOneShot(pulo);
            }

            VerificaObjetivos();
        }
        else
        {
            telaGameOver.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CuboBrilhante"))
        {
            Destroy(other.gameObject);
            audioPlayer.PlayOneShot(pegaCubo);
            pontos++;
            textoPontos.text = pontos.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {
            estaVivo = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("MoveP"))
        {
            gameObject.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MoveP"))
        {
            gameObject.transform.parent = null;
        }
    }
    private void VerificaObjetivos()
    {
        int totalCubos = Int32.Parse(textoTotal.text);
        TextMeshProUGUI objetivo = GameObject.Find("Objetivo").GetComponent<TextMeshProUGUI>();
        Image emoji = GameObject.Find("Emoji").GetComponent<Image>();

        Debug.LogFormat($"Pontos: {pontos}, Total Cubos: {totalCubos}");
        
        if(pontos < totalCubos)
        {
            emoji.sprite = emojis[0];
            objetivo.text = "Pegue todos os Cubos!";
        }
        
        if(pontos >= totalCubos / 2)
        {
            emoji.sprite = emojis[1];
            objetivo.text = "Continue assim!";
        }
        
        if(pontos >= totalCubos - 5)
        {
            emoji.sprite = emojis[2];
            objetivo.text = "Quase no fim!";
        }
        
        if(pontos == totalCubos)
        {
            emoji.sprite = emojis[3];
            objetivo.text = "Todos os Cubos coletados, passagem liberada!";
        }
    }

}
