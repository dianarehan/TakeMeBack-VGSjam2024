using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MessageDisplay : MonoBehaviour
{
    public GameObject textMeshPro;
    private TextMeshProUGUI label;
    public string message = "Hello, world!";

    void Start()
    {   
        label = textMeshPro.GetComponent<TextMeshProUGUI>();
        
    }

    public IEnumerator DisplayMessage()
    {
        foreach (char c in message)
        {
            label.text += c;
            yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
        }
    }
}
