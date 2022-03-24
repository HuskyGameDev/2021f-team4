<<<<<<< Updated upstream
=======
using System;
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCustomers : MonoBehaviour
{
    public GameObject customerPrefab; //Reference to customer prefab
    public float respawnTime = 3.0f; //How often to spawn customers.
    private Vector2 screenBounds;
<<<<<<< Updated upstream
=======
    public static int customerNum = 0;
    public GameObject[] CustomerQueue = new GameObject[5];
    public CustomerMovement customerMovement;
>>>>>>> Stashed changes


    //public Queue<GameObject> CustomerQueue;
    //public string CustomerName;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));
        StartCoroutine(customerWave());
    }

    // Method to spawn the customer on screen in a specific location.
    public void spawnCustomer()
    {
<<<<<<< Updated upstream
        GameObject customer = Instantiate(customerPrefab) as GameObject;
        customer.transform.position = new Vector2((float) -3.4, 6);
        //cust.transform.position.Set(1, 1, 1);
=======
        if (customerNum < 5)
        {
        GameObject customer = Instantiate(customerPrefab) as GameObject;
          customer.transform.position = startPos;  
            CustomerQueue[customerNum] = customer;
            customerNum++;
            
            Debug.Log(customer.name);
        }
>>>>>>> Stashed changes
    }

    //Timing for when customers spawn.
    IEnumerator customerWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnCustomer();
        }
    }
}
