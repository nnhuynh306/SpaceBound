using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovementController playerMovementController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killEnemy(GameObject enemy) {
        enemy.GetComponent<EnemyController>().killed();
        playerMovementController.grounded();
        playerMovementController.jumpWithoutCharge();
    }

     //--- ON TRIGGER ---------------------------------------------------------------------------- //
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (detectChildTrigger(other)) {
            return;
        }

        if (other.gameObject.CompareTag("Ground")) {
            playerMovementController.OnTriggerEnterGround();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (detectChildTrigger(other)) {
            return;
        }

        if (other.gameObject.CompareTag("Ground")) {
            playerMovementController.OnTriggerStayGround();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (detectChildTrigger(other)) {
            return;
        }

        if (other.gameObject.CompareTag("Ground")) {
            playerMovementController.OnTriggerExitGround();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            hitEnemy(other.gameObject);
        }
    }


    private bool detectChildTrigger(Collider2D other) {
        if (other.gameObject.CompareTag("Player's Detector")) {
            return true;
        } else {
            return false;
        }
    }

    private void hitEnemy(GameObject enemy) {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        if (!enemyController.IsKilled) {
            playerMovementController.knockback(enemy);
            killed();
        }
    }

    private void killed() {
        Debug.Log("Killed");
    }
}
