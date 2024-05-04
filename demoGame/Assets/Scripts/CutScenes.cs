using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenes : MonoBehaviour
{

    public GameObject textMeshPro;
    
    private TextMeshProUGUI label;
    string[] message = { "\nYou went to sleep in your comfortable bed, but woke up in an ancient castle.\r\n\nthe architecture really takes you back to ancient times, the kind of things that only exists in fantasy novels", 
        "\nIn this castle lived a \"great\" king, as all kings are called, but with power comes responsibility, and alas, also comes greed.\r\nThe king was not special enough to not succumb to that greed and amassed wealth from the people to build a massive castle.", 
        "\nYou reach the end, but you come up empty-handed. No treasure or interesting lore came out of this small adventure, maybe just a little sense of satisfaction from the thrill of exploration and enjoying \"Diana's\" Music ofc .\r\n\nIn the end, the castle was as empty as the consumed soul of its creator.\r\n\r\nYou wake up in your warm, fluffy bed." };
    public int sceneNumber=0;
    bool done = false;
    AudioSource audioSource;
    //public AudioClip clip;

    void Start()
    {
        label = textMeshPro.GetComponent<TextMeshProUGUI>();
        done = false;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(DisplayMessage());
        
    }
    private void Update()
    {

        if (done)
        
        {
            if(SceneManager.GetActiveScene().name=="Cutscene3")
                SceneManager.LoadScene("mainMenu");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

    }
    public IEnumerator DisplayMessage()
    {

        audioSource.Play();
        foreach (char c in message[sceneNumber])
        {
            label.text += c;

            yield return new WaitForSeconds(0.09f); // Wait for 0.2 seconds
            
        }
        audioSource.Stop();
        yield return new WaitForSeconds(3f);
        
        label = null;
        done = true;
        
    }
}
