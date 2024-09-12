using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    private AudioSource audio;
    private bool estaVivo = true;
    private int pontos = 0;
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
        audio = GetComponent<AudioSource>();
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
            
            //rb.AddForce(new Vector3(moveH, 0, moveV) * velocidade);

            //Pulo
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
                audio.PlayOneShot(pulo);
                //isJumping = true;
            }
        }

        if(!estaVivo)
        {
            rb.Sleep();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Piso"))
        {
            audio.PlayOneShot(queda);
        }

        if(other.gameObject.CompareTag("Lava"))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            estaVivo = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Moeda"))
        {
            audio.PlayOneShot(moeda);
            pontos++;
            Destroy(other.gameObject);
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
}
