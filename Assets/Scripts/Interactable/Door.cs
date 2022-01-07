using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Door : MonoBehaviour
{
    bool playerEntered = false;

    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Interact.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void interact(InputAction.CallbackContext context);



    private void OnTriggerEnter2D(Collider2D other) {
        if (!playerEntered && other.gameObject.CompareTag("Player's Body Collider")) {
            playerInput.Player.Interact.performed += interact;
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (playerEntered && other.gameObject.CompareTag("Player's Body Collider")) {
            playerInput.Player.Interact.performed -= interact;
            playerEntered = false;
        }
    }
}
