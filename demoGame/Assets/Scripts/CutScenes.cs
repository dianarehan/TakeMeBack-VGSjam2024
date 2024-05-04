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
    string[] message = { "Not All Doors are Exit, You gotta watch out!" };
    public int sceneNumber=0;
    bool done = false;
    void Start()
    {
        label = textMeshPro.GetComponent<TextMeshProUGUI>();
        done = false;
        StartCoroutine(DisplayMessage());
    }
    private void Update()
    {

        if (done)
        
        {
            SceneManager.LoadScene("level 1");
        }

    }
    public IEnumerator DisplayMessage()
    {
        

        foreach (char c in message[sceneNumber])
        {
            label.text += c;
            yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
        }
        yield return new WaitForSeconds(3f);
        
        label = null;
        done = true;
    }
}
