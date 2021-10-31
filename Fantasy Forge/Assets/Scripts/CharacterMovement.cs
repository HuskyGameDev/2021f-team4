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

    
    // Update is called once per frame
    void Update()
    {
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
