using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    public Vector2 movement;

    public bool canMove; // using in the TextBoxManager script to stop player from moving while interacting
    /*public float CharacterXPosition;
    public float CharacterYPosition;
    public float CharacterZPosition;
    
    public float currentPosition(){
        CharacterXPosition = PlayerPrefs.GetFloat("CharacterPositionX");
        return CharacterXPosition;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!canMove) // player movement is stopped
        {
            return;
        }
        
        //Input
        /*PlayerPrefs.SetFloat("CharacterPositionX",transform.position.x);*/

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //print(GameObject.Find("Blacksmith").transform.position.y);
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
  }
}
