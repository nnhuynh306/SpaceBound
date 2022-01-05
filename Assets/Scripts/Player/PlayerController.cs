using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovementController playerMovementController;

    PlayerHealthController playerHealthController;

    private bool isInvincible = false;

    private bool shieldActivated = false;

    private float bodyAndHeadColliderCollisionTimer = 0f;
    private float bodyAndHeadColliderCollisionTime = 0.1f;

    private bool bodyAndHeadColliderCollided = false;
    
    private GameObject shieldEffect;
    private Coroutine breakShieldCoroutine;

    private bool isFinishing = false;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerHealthController = GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        bodyAndHeadColliderCollisionTimeCheck();

        checkFinishingAnimation();
    }

    void checkFinishingAnimation() {
        if (isFinishing) {
            playerTransform.Rotate(new Vector3(0, 0, -1f));
            playerTransform.localScale = playerTransform.localScale * 0.994f;
        }
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
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player's Detector"
         ||otherTag == "Player's Head Collider" 
         ||otherTag == "Player's Body Collider") {
            return true;
        } else {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (bodyAndHeadDoubleCollisionCheck(other)) {
            return;
        }
        Debug.Log(other.gameObject.tag);
        OnDamagedCheck(other);
    }

    private void bodyAndHeadColliderCollisionTimeCheck() {
        if (bodyAndHeadColliderCollided) {
            bodyAndHeadColliderCollisionTimer += Time.deltaTime;

            if (bodyAndHeadColliderCollisionTimer >= bodyAndHeadColliderCollisionTime) {
                bodyAndHeadColliderCollided = false;
                bodyAndHeadColliderCollisionTime = 0;
            }
        }
    }

    //use to prevent when head and body collider collide with the same object at the same time
    private bool bodyAndHeadDoubleCollisionCheck(Collision2D other) {
        string tag = other.otherCollider.gameObject.tag;
        if (tag != "Player's Head Collider" && tag != "Player's Body Collider") {
            return false;
        }

        if (bodyAndHeadColliderCollided) {
            return true;
        } else {
            bodyAndHeadColliderCollided = true;
            return false;
        }
    }

    private void OnDamagedCheck(Collision2D other) {
       if (!isInvincible) {
            if (other.gameObject.CompareTag("Enemy")) {
                hitEnemy(other.gameObject);
            }
            if (other.gameObject.CompareTag("LethalObject")) {
                hitLethalObject(other.gameObject);
            }
       }
    }

    public void hitEnemy(GameObject enemy) {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        if (!enemyController.IsKilled) {
            if (shieldActivated) {
                breakShield(gameObject);
                return;
            }

            playerMovementController.knockback(enemy);
            playerHealthController.damaged(1);
        }
    }

    public void hitLethalObject(GameObject gameObject) {
        if (shieldActivated) {
            breakShield(gameObject);
            return;
        }

        playerMovementController.knockback(gameObject);
        playerHealthController.damaged(1);
    }

    private void killed() {
        Debug.Log("Killed");
    }
    
    public void invincible() {
        isInvincible = true;
    }

    public void vincible() {
        isInvincible = false;
    }

    //--- SKILLS-------------------------------------------------------------//

    //--SHIELD ----------//
    public void shield(float time) {
        shieldActivated = true;
        shieldEffect = Instantiate(Resources.Load<GameObject>("Effects/ShieldEffect"), new Vector2(-0.07f, -0.3f), Quaternion.identity);
        shieldEffect.transform.SetParent(this.gameObject.transform, false);

        breakShieldCoroutine = StartCoroutine("breakShieldCoroutineMethod", time);
    }

    public void breakShield(GameObject other) {
        shieldActivated = false;

        Destroy(shieldEffect);
    
        if (other != null) {
            StopCoroutine(breakShieldCoroutine);
            playerMovementController.applyKnockbackForce(other);
        }
    }

    public IEnumerator breakShieldCoroutineMethod(float time) {
        yield return new WaitForSeconds(time);
        breakShield(null);
    }

    public void finish() {
        playerMovementController.disableMovement();
        playerTransform = GetComponent<Transform>();
        Invoke("destroySelf", 1);
        isFinishing = true;
        
        GetComponent<PlayerSkillController>().disable();
    }
    
    private void destroySelf() {
        Destroy(gameObject);
    }
}
