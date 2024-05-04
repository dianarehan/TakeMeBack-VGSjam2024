using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    [SerializeField] AudioClip buttonSound;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    public void EndGame()
    {
        Application.Quit();
    }
    public void soundButton()
    {   if(audioSource != null && buttonSound!=null)
        audioSource.PlayOneShot(buttonSound);
    }
}
