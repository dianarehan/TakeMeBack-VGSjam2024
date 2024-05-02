using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneMngr : MonoBehaviour
{
    public RawImage blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        blackScreen.CrossFadeAlpha(0, 7f, true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
