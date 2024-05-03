using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MessageDisplay : MonoBehaviour
{
    public GameObject textMeshPro;
    public GameObject panelDisplayer;
    private TextMeshProUGUI label;
    public string message = "Not All Doors are Exit lmao";
    bool done = false;
    void Start()
    {   
        label = textMeshPro.GetComponent<TextMeshProUGUI>();
        panelDisplayer.SetActive(false);
    }
    private void Update()
    {
        if(done)
            panelDisplayer.SetActive(false);

    }
    public IEnumerator DisplayMessage()
    {
        panelDisplayer.SetActive(true);
        foreach (char c in message)
        {
            label.text += c;
            yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
        }
        yield return new WaitForSeconds(3f);
        done = true;
    }
}
