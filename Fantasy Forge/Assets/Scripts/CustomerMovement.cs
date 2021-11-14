using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private float moveSpeed = 4f; //Customer speed
    public Rigidbody2D customerRB;
    public Animator animator; //Allows for animations of customer
    private Vector2 movement;
    private Vector2 screenBounds;



    // Start is called before the first frame update
    void Start()
    {
        //Hiding customer before it appears
        gameObject.GetComponent<Renderer>().enabled = false;

        //Allows for a wait time to be implemented(for patience)
        StartCoroutine(patience());

        //Adding screenbound so customer disappears after leaving the screen.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));
    }

    // The Behavior of the customers
    IEnumerator patience()
    {

        //Wait for customer to enter and start walking
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(1);
        movement.x = 1;

        //Customer waiting for item to be made(patience)
        yield return new WaitForSeconds(10);

        //Customer either receives item or loses patience and leaves.
        movement.x = 0;
        yield return new WaitForSeconds(1);
        movement.y = 1;
        yield return new WaitForSeconds(.5f);
        movement.y = 0;
        movement.x = -1;
    }


    // Update is called once per frame
    void Update()
    {
        //Checking to see if customer is to the left of the screen.
        if (transform.position.x < screenBounds.x)
        {
            Destroy(this.gameObject);
        }

        //Animation for customer to walk in different directions
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    // Detects if the customer runs into the desk or the door
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Desk")
        {
            movement.x = 0;
        }
    }


    private void FixedUpdate()
    {
        //Movement for the costumer
        customerRB.MovePosition(customerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
