using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMove : MonoBehaviour
{
    [SerializeField] private GameObject ponto1;
    [SerializeField] private GameObject ponto2;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int dirP = 1;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * speed * dirP);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plataforma"))
        {
            if (dirP == 1)
            {
                dirP = -1;
            }
            else
            {
                dirP = 1;
            }
            
        }
    }
}
