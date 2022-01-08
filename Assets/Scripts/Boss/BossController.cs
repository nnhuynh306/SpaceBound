using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame updat
    
    public int killedGold = 1000;

    SpriteDelayBlinkingController spriteDelayBlinkingController;

    void Start()
    {
        spriteDelayBlinkingController = GetComponent<SpriteDelayBlinkingController>();
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
}
