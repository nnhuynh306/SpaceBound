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
        EnemyController enemyController =  enemy.GetComponent<EnemyController>();
        Debug.Log(!enemyController.IsKilled);
        if (!enemyController.IsKilled) {
            enemyController.killed();
            playerMovementController.grounded();
            playerMovementController.jumpWithoutCharge();
        }
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

    private bool detectChildTrigger(Collider2D other) {
        if (other.gameObject.CompareTag("Player's Detector")) {
            return true;
        } else {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            hitEnemy(other.gameObject);
        }
        if (other.gameObject.CompareTag("LethalObject")) {
            hitLethalObject(other.gameObject);
            Debug.Log("hit spike");
        }
    }

    public void hitEnemy(GameObject enemy) {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        if (!enemyController.IsKilled) {
            playerMovementController.knockback(enemy);
            killed();
        }
    }

    public void hitLethalObject(GameObject gameObject) {
        playerMovementController.knockback(gameObject);
    }

    private void killed() {
        Debug.Log("Killed");
    }

}
