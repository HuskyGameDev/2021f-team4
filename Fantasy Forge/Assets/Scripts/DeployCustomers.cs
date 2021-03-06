using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCustomers : MonoBehaviour
{
    public GameObject customerPrefab; //Reference to customer prefab
    public float respawnTime = 1.0f; //How often to spawn customers.
    public Vector2 startPos;
    private Vector2 screenBounds;
    public static int customerNum = 0;
    public GameObject[] CustomerQueue = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));
        StartCoroutine(customerWave());
    }

    // Method to spawn the customer on screen in a specific location.
    private void spawnCustomer()
    {
        if (customerNum < 4)
        {
            GameObject customer = Instantiate(customerPrefab) as GameObject;
            customer.transform.position = startPos;
              CustomerQueue[customerNum] = customer;
            
            customerNum++;
        }
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
