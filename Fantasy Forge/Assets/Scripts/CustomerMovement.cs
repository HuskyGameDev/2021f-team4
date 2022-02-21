using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CustomerMovement : MonoBehaviour
{
    public GameObject character_movement;
    //public CharacterMovement character_movement_script;
    private float moveSpeed = 4f; //Customer speed
    public Rigidbody2D customerRB;
    public Animator animator; //Allows for animations of customer
    private Vector2 movement;
    private Vector2 screenBounds;
    private float RandomNum = 0;
 //   public float CharacterXPosition;
    //public float CharacterYPosition;
  //  public float CharacterZPosition;

    // Start is called before the first frame update
    void Start()
    {
        //character_movement_script = character_movement.GetComponent<CharacterMovement>();
       // transform.position = new Vector3 (CharacterXPosition,CharacterYPosition,CharacterZPosition);
       
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
        RandomNum=  UnityEngine.Random.Range(10.0f,20.0f);
        movement.x = 1;
    }

    // The patience of the customers
    IEnumerator patience()
    {
        //Customer waiting for item to be made(patience)        
        yield return new WaitForSeconds(RandomNum);

        //Customer leaving after losing patience
        this.movement.x = 1;
    }

    // Update is called once per frame
    void Update()
    {
      //PlayerPrefs.SetFloat("MyPositionX", transform.position.x);
      // PlayerPrefs.SetFloat("MyPositionY", transform.position.y);
      // PlayerPrefs.SetFloat("MyPositionZ", transform.position.z);
      var positionX  = GameObject.Find("Blacksmith").transform.position.x;
      var positionY = GameObject.Find("Blacksmith").transform.position.y;

        if(Input.GetKeyDown("e") &&  positionX > 1.2f && positionX < 4.3f && positionY >4.0f){
            print("e key was pressed");
            print(positionX);
            movement.x = 1;
        }

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
    private void OnCollisionEnter2D(Collision2D collision)
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


    private void FixedUpdate()
    {
        //Movement for the costumer
        customerRB.MovePosition(customerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
