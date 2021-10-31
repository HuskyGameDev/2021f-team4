using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public int movementEnabled = 1;   // Used to disable movement when minigames open

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    public bool canMove; // using in the TextBoxManager script to stop player from moving while interacting
    
    // Update is called once per frame
    void Update()
    {
        if (!canMove) // player movement is stopped
        {
            return;
        }

        //Input
        movement.x = Input.GetAxisRaw("Horizontal") * movementEnabled;
        movement.y = Input.GetAxisRaw("Vertical") * movementEnabled;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
