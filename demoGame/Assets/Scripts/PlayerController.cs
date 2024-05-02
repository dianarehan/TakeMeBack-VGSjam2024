using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float speed = 5f;
    float verticalSpeed = 3f;
    bool isFacingRight =true;
    

    void Start()
    {
       
    }

    void Update()
    {
        float horizontalSignal = Input.GetAxis("Horizontal");
        this.transform.Translate(new Vector2(horizontalSignal*Time.deltaTime*speed, 0));
        if (!isFacingRight && horizontalSignal > 0)
            Flip();
        else if (isFacingRight && horizontalSignal < 0)
            Flip();

        float verticalSignal = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector2(0, verticalSignal*Time.deltaTime*speed));


       
        
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 myScale = this.transform.localScale;
        myScale.x *= -1;
        this.transform.localScale=myScale;
    }

}
