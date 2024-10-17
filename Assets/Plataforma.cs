using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    
    [SerializeField] private float velocidade = 5.0f;
    [SerializeField] private int dirMove = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(
                                    dirMove * velocidade * Time.deltaTime,
                                    0,
                                    0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Plataforma"))
        {
            if(dirMove == 1)
            {
                dirMove = -1;
            }
            else
            {
                dirMove = 1;
            }
        }
    }

}
