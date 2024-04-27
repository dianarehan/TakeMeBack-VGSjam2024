using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }
    
    public void EndGame()
    {
        Application.Quit();
    }
    public void GetCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
