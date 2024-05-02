using UnityEngine;
using TMPro; 

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
        Debug.Log(textMeshPro2 != null); 
    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(clip);
    }
    public void UpdateScore(int currScore)
    {
       
        if (textMeshPro2 != null)
        {
            
            textMeshPro2.text = "Score: " + currScore;
        }
    }
}
