using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public KeyCode interactKey;         // Keycode used to open/close prompt    
    public GameObject promptPrefab;     // Prefab instantiated to promt user for future action such as 

    private bool _isInRange  = false;           // Indicates of player is within interact zone
    private bool _promptOpen = false;           // Indicates if prompt is open
    private GameObject _promptInstance = null;  // Actual instance of prompt prefab to be created/destroyed on open/close
    private CharacterMovement _player;          // Player object for enabling/disabling movement on prompt open


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                // Only attempt to open prompt if no other prompt is open/has frozen player
               // if (_player.movementEnabled == 1 || _promptOpen)
                //{
                    // Toggle prompt openness, player movement, etc.
                    if (!_promptOpen)
                    {
                        openPrompt();
                    }
                    else
                    {
                    closePrompt();
                    }
                //}
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
            //Debug.Log("Player now in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInRange = false;
            //Debug.Log("Player now not in range");
        }
    }

    public void openPrompt()
    {
        _player.canMove = false;
        _player.movement = Vector2.zero;
        _player.animator.SetFloat("Speed", _player.movement.sqrMagnitude);
        _promptOpen = true;
        _promptInstance = Instantiate(promptPrefab);
        _promptInstance.GetComponent<Prompt>().promptingInteractable = this;

        // Open prompt 5 units above scene
        _promptInstance.transform.position = Vector3.back * 5;
    }

    public void closePrompt()
    {
        _player.canMove = true;
        _promptOpen = false;
        Destroy(_promptInstance);
    }

}
