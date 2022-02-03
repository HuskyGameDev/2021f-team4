using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private float moveSpeed = 2f; //Customer speed
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
        StartCoroutine(appear());

        //Adding screenbound so customer disappears after leaving the screen.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));
    }


    // The starting behavior of customers
    IEnumerator appear()
    {
        yield return new WaitForSeconds(1);
        movement.x = 0;
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(1);
        movement.x = 1;
    }

    // Adding a buffer second to make line movement look more fluid
    IEnumerator second()
    {
        yield return new WaitForSeconds((float) .5);
        movement.x = 1;
    }

    // The patience of the customers
    IEnumerator patience()
    {
        //Customer waiting for item to be made(patience)
        yield return new WaitForSeconds(10);

        //Customer leaving after losing patience
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        movement.x = 1;
    }


    // Update is called once per frame
    void Update()
    {
        //Checking to see if customer is to the right of the screen.
        if (transform.position.x > screenBounds.x * -.8)
        {
            Destroy(this.gameObject);
        }

        //Animation for customer to walk in different directions
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }


    // Detects if the customer runs into the object at the counter or another customer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Customer(Clone)")
        {
            movement.x = 0;
        }

        if (collision.gameObject.name == "Front")
        {
            movement.x = 0;
            StartCoroutine(patience());
        }
    }

    // Detects if the customer in front has walked away
    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(second());
    }


    private void FixedUpdate()
    {
        //Movement for the costumer
        customerRB.MovePosition(customerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
