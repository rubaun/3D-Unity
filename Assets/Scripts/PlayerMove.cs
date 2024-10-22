using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    private AudioSource audioP;
    private bool estaVivo = true;
    private int pontos = 0;
    private Diretor diretor;
    private bool isJumping;
    private bool doubleJump;
    private int countJump = 0;
    private Vector3 posicaoPlayer;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    [Header("Sons do Personagem")]
    [SerializeField] private AudioClip pulo;
    [SerializeField] private AudioClip queda;
    [SerializeField] private AudioClip moeda;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioP = GetComponent<AudioSource>();
        diretor = GameObject.FindObjectOfType<Diretor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(estaVivo)
        {
            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");
            
            transform.position += new Vector3(
                                        moveH * velocidade * Time.deltaTime, 
                                        0, 
                                        moveV * velocidade * Time.deltaTime
                                        );

            
            //Pulo
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Jump();
                countJump++;
                isJumping = true;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && isJumping)
            {
                countJump++;
            }

            //Pulo Duplo
            if (isJumping && countJump == 2 && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                doubleJump = true;    
            }
        }

        if(!estaVivo)
        {
            rb.Sleep();
        }

        posicaoPlayer = transform.position;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Piso") || other.gameObject.CompareTag("MoveP"))
        {
            audioP.PlayOneShot(queda);
            isJumping = false;
            doubleJump = false;
            countJump = 0;
        }

        if(other.gameObject.CompareTag("Lava"))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
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
        gameObject.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Moeda"))
        {
            audioP.PlayOneShot(moeda, 0.5f);
            pontos++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Saida") && diretor.PodePassarFase())
        {
            diretor.ProximaFase();
        }

        if(other.gameObject.CompareTag("CheckPoint"))
        {

        }
    }

    public bool VerificaSePlayerEstaVivo()
    {
        return estaVivo;
    }

    public int ContagemPontos()
    {
        return pontos;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
        audioP.PlayOneShot(pulo);
    }

    public Vector3 GetPosicaoPlayer()
    {
        return posicaoPlayer;
    }

    public int GetPontos()
    {
        return pontos;
    }

    public int GetCenaAtual()
    {
        return diretor.GetCenaAtual();
    }

    public void SetPosicaoPlayer(Vector3 posicao)
    {
        posicaoPlayer = posicao;
    }

    public void SetPontos(int pontos)
    {
        this.pontos = pontos;
    }
}


