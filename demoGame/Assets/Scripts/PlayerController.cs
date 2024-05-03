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
    bool donePrinting = false;
    Rigidbody2D rb;


    int fragmentScore = 0;
    private int finalScore;


    public GameObject camera;
    LevelManager manager;
    AudioSource audioSource;
    public AudioClip portalSound;
    private GameObject portal2Object;

    MessageDisplay messageDisplay;



    public GameObject moveStuff;
    MoveStuff moveStuff2;
    bool goMovePlatfrom = false;


    bool foundFakeDoor = false;
    void Start()
    {
        LoadAccumulatedScore();
        rb = this.GetComponent<Rigidbody2D>();
        manager =camera.GetComponent<LevelManager>();
        messageDisplay= camera.GetComponent<MessageDisplay>();
        audioSource = camera.GetComponent<AudioSource>();
        portal2Object = GameObject.Find("SecondPortal");
        //gameObject.transform.position= checkPoints[0].transform.position;
        moveStuff2 = moveStuff.GetComponent<MoveStuff>();

        Debug.Log(fragmentScore);
        Debug.Log("Accumulated Score: " + PlayerPrefs.GetInt("AccumulatedScore", 0));
        Debug.Log(portal2Object.name);
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
        if (foundFakeDoor && !donePrinting)
        {
            StartCoroutine(messageDisplay.DisplayMessage());
            donePrinting = true;
        }
        if(fragmentScore==4)
            CompleteLevel();

        if (goMovePlatfrom)
        {
            moveStuff2.MovePlatfrom();

        }
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
        if (collision.CompareTag("Portal1"))
        {
            StartCoroutine(TeleportAfterDelay());
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
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            goMovePlatfrom = true;
            
        }
        if (collision.gameObject.CompareTag("FakeDoor"))
        {
            foundFakeDoor = true;

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
        
        if(SceneManager.GetActiveScene().name=="level2")
            SceneManager.LoadScene("mainMenu");
        else
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(camera);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
      IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait for one second

        // Move the object to the position of portal2Object
        this.gameObject.transform.localPosition = portal2Object.transform.localPosition;

        // Play the portal sound
        audioSource.PlayOneShot(portalSound);
    }
}
