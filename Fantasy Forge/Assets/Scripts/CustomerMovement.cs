using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CustomerMovement : MonoBehaviour
{
<<<<<<< Updated upstream
    public GameObject character_movement;
=======
>>>>>>> Stashed changes
    private float moveSpeed = 4f; //Customer speed
    public Rigidbody2D customerRB;
    public Animator animator; //Allows for animations of customer
    public Vector2 movement;
    private Vector2 screenBounds;
    private float RandomNum = 0;
<<<<<<< Updated upstream
 
=======
    private InventoryItem _inputItem;
    public int complete = 0;
    public DeployCustomers deployCustomers;
    private int customerNumber;
    private string CustomerName;
    
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
       
=======
      
>>>>>>> Stashed changes
        //Hiding customer before it appears
        gameObject.GetComponent<Renderer>().enabled = false;

        //Allows for a wait time to be implemented(for patience)
        StartCoroutine(appear());

        //Adding screenbound so customer disappears after leaving the screen.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));
    }

    // The starting behavior of customers
    IEnumerator appear()
<<<<<<< Updated upstream
    {   
        
=======
    {
      
>>>>>>> Stashed changes
        yield return new WaitForSeconds(1);
        movement.x = 0;
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(1);
        RandomNum=  UnityEngine.Random.Range(10.0f,20.0f);
        movement.x = 1;
  
    }

    // The patience of the customers
    IEnumerator patience()
    {
        //Customer waiting for item to be made(patience)        
        yield return new WaitForSeconds(RandomNum);

        //Customer leaving after losing patience
        movement.x = 1;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
      var positionX  = GameObject.Find("Blacksmith").transform.position.x;
      var positionY = GameObject.Find("Blacksmith").transform.position.y;
    

        if(Input.GetKeyDown("e") &&  positionX > 1.2f && positionX < 4.3f && positionY >4.0f ){
            this.movement.x = 1;
        }

        //Checking to see if customer is to the right of the screen.
        if (transform.position.x > screenBounds.x * -.8)
=======
     var positionX  = GameObject.Find("Blacksmith").transform.position.x;
     var positionY = GameObject.Find("Blacksmith").transform.position.y;
     if (Input.GetKeyUp("e") && positionX > 1.2 && positionX < 3.4 && positionY > 3.0 )
        {
           movement.x =1;
        }
                //Checking to see if customer is to the right of the screen.
        if (transform.position.x > screenBounds.x * -.45)
>>>>>>> Stashed changes
        {
            Destroy(this.gameObject);
        }

        //Animation for customer to walk in different directions
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

<<<<<<< Updated upstream
    // Detects if the customer runs into the object at the counter or another customer
    private void OnCollisionEnter2D(Collision2D collision)
    {
=======
    // Detects if the customer in front has walked away
    private void OnTriggerExit2D(Collider2D collision)
    {
        movement.x = 1;
    }

    // Detects if the customer encounters the trigger for the front desk or another customer
    private void OnTriggerEnter2D(Collider2D collision)
    {    
     
    
     print(collision.gameObject.name);
>>>>>>> Stashed changes
        if (collision.gameObject.name == "Customer(Clone)")
        {
            
            movement.x = 0;
        }
        if(collision.gameObject.name == "Character"){

        }
        if (collision.gameObject.name == "Front")
        {
      
            movement.x = 0;
            StartCoroutine(patience());
        }
    }
    
    
  


    private void FixedUpdate()
    {
        //Movement for the costumer
        customerRB.MovePosition(customerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
