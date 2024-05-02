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
    bool youCanPickItem = false;
    [SerializeField] GameObject itemDisplayer;
    GameObject itemToBeDisplayed;
    GameObject newItem;
    private Sprite pickedItemSprite;
    //public RawImage blackScreen;

    void Start()
    {
        itemDisplayer.GetComponent<Image>();
        //blackScreen.CrossFadeAlpha(0, 7f, true);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSignal = Input.GetAxis("Horizontal");
        this.transform.Translate(new Vector2(horizontalSignal*Time.deltaTime*speed, 0));
        float verticalSignal = Input.GetAxis("Vertical");
        this.transform.Translate(new Vector2(0, verticalSignal*Time.deltaTime*speed));


        //here i am in range of the item so i have the option to press E and pick it up
        if (youCanPickItem)
        {   //here if the player presses E the item is picked then it gets shown on the canvas
            if (Input.GetKeyDown(KeyCode.E))
            {   // i save the item's sprite in a variable before destroying it to use it later in putting it on the canvas
                pickedItemSprite = itemToBeDisplayed.GetComponent<SpriteRenderer>().sprite;

                itemDisplayer.GetComponent<Image>().sprite = pickedItemSprite; 
                //i picked the item so i destroy it
                Destroy(itemToBeDisplayed.gameObject);
    
             }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Item") )
        {   
            itemToBeDisplayed = other.gameObject;
            newItem = other.gameObject;
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            youCanPickItem=true;

            

        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            youCanPickItem=false; //beacuse i am not in range of the item, I cant pick it up
        }
    }


}
