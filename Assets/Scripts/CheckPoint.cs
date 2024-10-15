using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private SaveData diretor;


    // Start is called before the first frame update
    void Start()
    {
        diretor = GameObject.Find("Diretor").GetComponent<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        diretor.Save();
    }
}
