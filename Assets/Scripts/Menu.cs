using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip play;
    private AudioSource playSource;

    private void Awake()
    {
        playSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        playSource.PlayOneShot(play);
        SceneManager.LoadScene("Fase1");
    }

    public void Creditos()
    {
        playSource.PlayOneShot(play);
        SceneManager.LoadScene("Creditos");
    }

    public void VoltarMenu()
    {
        if(playSource != null)
        {
            playSource.PlayOneShot(play);
        }
        
        SceneManager.LoadScene("Menu");
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
