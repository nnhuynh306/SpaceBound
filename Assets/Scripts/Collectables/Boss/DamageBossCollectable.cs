using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBossCollectable : MonoBehaviour
{   
    bool collected = false;
    public string collectedSoundName = "DamageBossCollectableCollected";

    private GameObject bullet;


    private void Start() {
        bullet = transform.Find("DamageBossBullet").gameObject;

        disableBulletBossInteraction();
    }

    private void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (checkPlayerTag(other.gameObject.tag)) {
            if (!collected) {
                collectedBy(other.gameObject);
                collected = true;
            }
        }
    }

    private bool checkPlayerTag(string tag) {
        return tag.Contains("Player");
    }

    public void collectedBy(GameObject player) {
        AudioManager.Instance.playOneAtATime(collectedSoundName);

        playBubbleBreakEffect();

        transform.Find("DamageBossBullet").GetComponent<DamageBossBullet>().released();

    }
    private void playBubbleBreakEffect() {
        Destroy(transform.Find("Bubble").gameObject);
        Instantiate(Resources.Load<GameObject>("Boss/WaterEffect"), Vector2.zero, Quaternion.identity)
            .transform.SetParent(this.transform, false);
        enableBulletBossLethalInteraction();
    }

     public void disableBulletBossInteraction() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Boss Interact"), LayerMask.NameToLayer("FlyingBoss"), true);
    }

    public void enableBulletBossLethalInteraction() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Boss Interact"), LayerMask.NameToLayer("FlyingBoss"), false);

    }

}
