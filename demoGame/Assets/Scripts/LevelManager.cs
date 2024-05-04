using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;
    public GameObject textMeshPro;
    private TextMeshProUGUI textMeshPro2; 
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
      
        textMeshPro2 = textMeshPro.GetComponent<TextMeshProUGUI>();
        
    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(clip);
    }
    public void UpdateScore(int currScore)
    {
       
        if (textMeshPro2 != null)
        {
            if(SceneManager.GetActiveScene().name =="level 1")
            {
                textMeshPro2.text = "Score: " + currScore + "/17";
            }
            else if (SceneManager.GetActiveScene().name =="level2")
            {
                textMeshPro2.text = "Score: " + currScore + "/23";
            }
            else
            {
                textMeshPro2.text = "Score: " + currScore ;
            }

        }
    }

    
}
