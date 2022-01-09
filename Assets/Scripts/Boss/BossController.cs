using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame updat
    
    public int killedGold = 1000;

    SpriteDelayBlinkingController spriteDelayBlinkingController;

    Rigidbody2D rigidBody;

    bool facingRight = false;

    bool isKilled = false;

    public bool IsKilled {
        set {
            isKilled = value;
        }
        get {
            return isKilled;
        }
    }

    void Start()
    {
        spriteDelayBlinkingController = GetComponent<SpriteDelayBlinkingController>();

        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killed() {
        stopMusic();

        StartCoroutine(startKilledAnimation());
    }

    private IEnumerator startKilledAnimation() {
        if (spriteDelayBlinkingController != null) {
            spriteDelayBlinkingController.startBlinking();
        }

        AudioManager.Instance.playOneAtATime("BossExplosionLoop");

        GameObject explosionLoop = Instantiate(Resources.Load<GameObject>("Boss/ExplosionLoop"), transform.position, Quaternion.identity);

        yield return new WaitForSeconds(3);

        if (spriteDelayBlinkingController != null) {
            spriteDelayBlinkingController.stopBlinking();
        }

        ending();

        AudioManager.Instance.stop("BossExplosionLoop");
        AudioManager.Instance.playOneAtATime("BossExplosionFinal");

        Destroy(explosionLoop);
        Instantiate(Resources.Load<GameObject>("Boss/FinalExplosion"), transform.position, Quaternion.identity);

    }

    private void ending() {
        Destroy(gameObject);
        PortalSpawner.Instance.spawn();
        FindObjectOfType<PlayerController>().collectMoney(killedGold);
    }

    public void stopMusic() {
        GameManager.Instance.stopTheme();
    }

    public void moveTowardPlayer(float speed) {
        Vector2 movePosition = rigidBody.position;
        movePosition = Vector2.MoveTowards(movePosition, FindObjectOfType<PlayerController>().gameObject.transform.position, speed * Time.fixedDeltaTime);
        flipCheck(rigidBody.position.x, movePosition.x);

        rigidBody.MovePosition(movePosition);
    }

    private void flipCheck(float currentX, float nextX) {
        if ((nextX - currentX) > 0 && !facingRight) {
            flip();
        } else if ((nextX - currentX) < 0 && facingRight) {
            flip();
        }
    }

    private void flip() {
        facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

}
