using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Diretor diretor;


    // Start is called before the first frame update
    void Start()
    {
        diretor = GameObject.FindObjectOfType<Diretor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        diretor.SavePlayer(FindObjectOfType<PlayerMove>());
        StartCoroutine("Contador");
    }

    IEnumerator Contador()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
