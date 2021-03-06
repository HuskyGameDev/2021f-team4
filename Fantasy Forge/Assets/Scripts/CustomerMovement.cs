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
    public Vector2 movement;
    private Vector2 screenBounds;
    private float RandomNum = 0;
    public int complete = 0;
    public InventoryItem desiredItem;
    private GameObject _desiredObject;
    public bool inFront;

    // Start is called before the first frame update
    void Start()
    {
        //character_movement_script = character_movement.GetComponent<CharacterMovement>();
       // transform.position = new Vector3 (CharacterXPosition,CharacterYPosition,CharacterZPosition);
       
        //Hiding customer before it appears
        gameObject.GetComponent<Renderer>().enabled = false;
        inFront = false;

        //Allows for a wait time to be implemented(for patience)
        StartCoroutine(appear());

        //Adding screenbound so customer disappears after leaving the screen.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));

        // Randomly generate desired item
        _desiredObject = null;
        desiredItem = new InventoryItem();
        desiredItem.itemState = ItemState.Sword;
        desiredItem.metalType = (MetalType)Random.Range(0, 3.99f);
        desiredItem.hiltType = (HiltType)Random.Range(0, 2.99f);
        Debug.Log("I want " + desiredItem.toString()) ;

    }

    // The starting behavior of customers
    IEnumerator appear()
    {
        yield return new WaitForSeconds(1);
        movement.x = 0;
        Sound.PlaySound("Bell Walk 2");  // Audio for when customer enters
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
        dismiss();
    }

    // Update is called once per frame
    void Update()
    {

      var positionX  = GameObject.Find("Blacksmith").transform.position.x;
      var positionY = GameObject.Find("Blacksmith").transform.position.y;
        if (Input.GetKeyUp("e") && positionX > 1.3f && positionX < 3.0f && positionY > 2.2f && complete == 1)
        {
            movement.x = 1;
        }
        

        //Checking to see if customer is to the right of the screen.
        if (transform.position.x > screenBounds.x * -.45)
        {
            Destroy(this.gameObject);
            DeployCustomers.customerNum--;
        }

        //Animation for customer to walk in different directions
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    // Detects if the customer in front has walked away
    private void OnTriggerExit2D(Collider2D collision)
    {
        movement.x = 1;
    }

    // Detects if the customer encounters the trigger for the front desk or another customer
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Customer(Clone)")
        {
            movement.x = 0;
        }

        if (collision.gameObject.name == "Front")
        {
            movement.x = 0;
            inFront = true;
            Debug.Log(this + "IS AT THE FRONT OF THE LINE");
            //StartCoroutine(patience()); // REMOVED FOR PLAYTESTING
        }

        if (_desiredObject == null)
        {
            _desiredObject = desiredItem.toGameObject();
            _desiredObject.transform.SetParent(gameObject.transform);
            _desiredObject.transform.position = gameObject.transform.position;
            _desiredObject.transform.localScale = Vector3.one;
        }
    }

    // Tell customer to leave
    public void dismiss()
    {
        Destroy(_desiredObject);
        inFront = false;
        movement.x = 1;
    }

    private void FixedUpdate()
    {
        //Movement for the customer
        customerRB.MovePosition(customerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
