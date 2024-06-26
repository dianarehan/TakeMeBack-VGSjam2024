using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MessageDisplay : MonoBehaviour
{
    public GameObject textMeshPro;
    public GameObject panelDisplayer;
    private TextMeshProUGUI label;
    public string message = "Not All Doors are Exit, You gotta watch out!";
    
    void Start()
    {   
        label = textMeshPro.GetComponent<TextMeshProUGUI>();
        panelDisplayer.SetActive(false);
    }
    private void Update()
    {
        
            

    }
    public IEnumerator DisplayMessage()
    {
        
        panelDisplayer.SetActive(true);
        
        foreach (char c in message)
        {
            label.text += c;
            yield return new WaitForSeconds(0.05f); // Wait for 0.2 seconds
        }
        yield return new WaitForSeconds(0.3f);
        panelDisplayer.SetActive(false);
        label = null;
        
    }
}
