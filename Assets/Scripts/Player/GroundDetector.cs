using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private PlayerMovementController playerMovementController;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementController = gameObject.GetComponentInParent<PlayerMovementController>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            return;
        }
        
        if (other.gameObject.CompareTag("Ground")) {
            playerMovementController.grounded();
        }

        if (other.gameObject.CompareTag("Enemy")) {
            killEnemy(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            return;
        }
        
        if (other.gameObject.CompareTag("Ground")) {
            playerMovementController.grounded();
        }
    }

    private void killEnemy(Collider2D other) {
        playerController.killEnemy(other.gameObject);
    }
}
