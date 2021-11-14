using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCustomers : MonoBehaviour
{
    public GameObject customerPrefab; //Reference to customer prefab
    public float respawnTime = 1.0f; //How often to spawn customers.
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.z));
        StartCoroutine(customerWave());
    }

    // Method to spawn the customer in a specific location on screen.
    private void spawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab) as GameObject;
        customer.transform.position = new Vector2(screenBounds.x * 1, screenBounds.y * 1); //Placement of customer, setting the x and y values.
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
