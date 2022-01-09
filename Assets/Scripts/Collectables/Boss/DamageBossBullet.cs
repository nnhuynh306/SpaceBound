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

    private float scaleTime = 2f;

    public Vector3 finalScale = new Vector3(2f, 2f, 2f);

    private Vector3 startScale;

    private Rigidbody2D rigidBody;

    public int damageDealt = 1;

    private bool destroyed = false;

    private void Start() {
        rigidBody =GetComponent<Rigidbody2D>();
        startScale = transform.localScale;
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
            hitBoss(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Boss") && !destroyed) {
            other.gameObject.GetComponent<BossHealthController>().damaged(damageDealt);
            hitBoss(other.gameObject);
        }
    }

    public void released() {
        scalingBullet = true;

        AudioManager.Instance.playOneAtATime(scalingSound);
    }

    private void scaleBullet() {
        if (scalingBullet) {
            if (Vector3.Magnitude(transform.localScale) < Vector3.Magnitude(finalScale)) {
                float scaleAmount = ((finalScale.x - startScale.x) * (Time.deltaTime / scaleTime));
                transform.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
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

            Vector2 movePosition = Vector2.MoveTowards(rigidBody.position, boss.transform.position,  movingSpeed * Time.fixedDeltaTime);
            rigidBody.MovePosition(movePosition);
        }
    }

    private void hitBoss(GameObject boss) {
        destroyed = true;

        AudioManager.Instance.stop(scalingSound);

        AudioManager.Instance.playOneAtATime("ElectricSound");

        Instantiate(Resources.Load<GameObject>(hitEffectPath), this.gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);

        if (gameObject.GetComponentInParent<DamageBossCollectable>() != null) {
            Destroy(gameObject.transform.parent);
        }
    }
}
