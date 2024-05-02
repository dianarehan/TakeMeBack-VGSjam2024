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
    float verticalSpeed = 9f;
    float jumpSpeed = 5f;

    bool isFacingRight =true;
    bool isFacingALadder = false;
    bool isGrounded = true;
    Rigidbody2D rb;


    int fragmentScore = 0;

    public GameObject camera;
    LevelManager manager;
    void Start()
    {
       rb = this.GetComponent<Rigidbody2D>();
       manager =camera.GetComponent<LevelManager>();

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
            if(isFacingALadder)
                this.transform.Translate(new Vector2(0, verticalSignal*Time.deltaTime* verticalSpeed));

        float jumpingSignal = Input.GetAxis("Jump");
        if (jumpingSignal > 0 && isGrounded)
            Jump();
       
        
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 myScale = this.transform.localScale;
        myScale.x *= -1;
        this.transform.localScale=myScale;
    }
    void Jump()
    {
        //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isFacingALadder = true;
        }
        else if (collision.CompareTag("fragment"))
        {
            AddScore(1);
            Destroy(collision.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isFacingALadder = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = false;
        }
    }
    void AddScore(int score)
    {
        fragmentScore++;
        manager.PlaySound();
        manager.UpdateScore(fragmentScore);
    }
    
}
