using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBossBullet : MonoBehaviour
{
    public string shootingSound = "BulletShooting";
    public string explodedSound = "";

    public string scalingSound = "BulletCharging";

    public string hitEffectPath = "Boss/DamageBossBulletHit";

    bool scalingBullet = false;
    private bool movingTowardBoss = false;

    public float movingSpeed = 20f;

    [Range(1.0001f, 2f)]
    public float scalingMultiplier = 1.001f;

    public Vector3 finalScale = new Vector3(2f, 2f, 2f);

    private Rigidbody2D rigidBody;

    public int damageDealt = 1;

    private bool destroyed = false;

    private void Start() {
        rigidBody =GetComponent<Rigidbody2D>();
    }

    private void Update() {
        scaleBullet();
    }

    private void FixedUpdate() {
        chaseBoss();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Boss") && !destroyed) {
            other.gameObject.GetComponent<BossHealthController>().damaged(damageDealt);
            hitBoss();
        }
    }

    public void released() {
        scalingBullet = true;

        AudioManager.Instance.playOneAtATime(scalingSound);
    }

    private void scaleBullet() {
        if (scalingBullet) {
            if (Vector3.Magnitude(transform.localScale) < Vector3.Magnitude(finalScale)) {
                transform.localScale *= scalingMultiplier;
            } else {
                scalingBullet = false;
                movingTowardBoss = true;

                AudioManager.Instance.stop(scalingSound);
                AudioManager.Instance.playOneAtATime(shootingSound);
            }
        }
    }

    private void chaseBoss() {
        if (movingTowardBoss) {
            GameObject boss = FindObjectOfType<BossController>().gameObject;

            Vector3 movePosition = rigidBody.position;
 
            movePosition.x = Mathf.MoveTowards(transform.position.x, boss.transform.position.x, movingSpeed * Time.fixedDeltaTime);
            movePosition.y = Mathf.MoveTowards(transform.position.y, boss.transform.position.y, movingSpeed * Time.fixedDeltaTime);
            
            rigidBody.MovePosition(movePosition);
        }
    }

    private void hitBoss() {
        destroyed = true;

        Instantiate(Resources.Load<GameObject>(hitEffectPath), Vector3.zero, Quaternion.identity).transform.SetParent(gameObject.transform, false);

        Destroy(gameObject);
    }
}