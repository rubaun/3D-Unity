using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    private bool isJumping;
    [SerializeField] private float velocidade;
    [SerializeField] private float velocidadeRotacao;
    [SerializeField] private float forcaPulo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        
        /*transform.position += new Vector3(
                                    moveH * velocidade * Time.deltaTime, 
                                    0, 
                                    moveV * velocidade * Time.deltaTime
                                    );*/
                                    

        //Andar
        rb.AddForce(Vector3.forward * velocidade * moveV);
        Vector3 force = transform.forward * moveV * velocidade * Time.fixedDeltaTime;
        
        rb.AddForce(force);

        //Virar
        float turn = moveH * velocidadeRotacao * Time.fixedDeltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * turnRotation);      
        
        //Pulo
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
            //isJumping = true;
        }

    }

    private void OnCollisionEnter(Collision other) 
    {
        isJumping = false;
    }
}
