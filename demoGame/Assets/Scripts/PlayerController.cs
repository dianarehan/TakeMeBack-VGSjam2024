using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private int finalScore;


    public GameObject camera;
    LevelManager manager;
    void Start()
    {
        LoadAccumulatedScore();
        rb = this.GetComponent<Rigidbody2D>();
        manager =camera.GetComponent<LevelManager>();
        Debug.Log(fragmentScore);
        Debug.Log("Accumulated Score: " + PlayerPrefs.GetInt("AccumulatedScore", 0));

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
       
        if(fragmentScore==7)
            CompleteLevel();
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
        fragmentScore+=score;
        manager.PlaySound();
        manager.UpdateScore(fragmentScore);
    }



    public void SaveLocalScore()
    {
        finalScore += fragmentScore;
        fragmentScore = 0; 
        SaveAccumulatedScore(); 
    }

    // Function to save the accumulated score
    private void SaveAccumulatedScore()
    {
        PlayerPrefs.SetInt("AccumulatedScore", finalScore);
        PlayerPrefs.Save();
    }

    // Function to load the accumulated score
    private void LoadAccumulatedScore()
    {
        finalScore = PlayerPrefs.GetInt("AccumulatedScore", 0); // 0 is the default value if the key doesn't exist
    }

    public void CompleteLevel()
    {
        SaveLocalScore(); // Save the local score to the accumulated score
        SceneManager.LoadScene("level2");              
    }
}
