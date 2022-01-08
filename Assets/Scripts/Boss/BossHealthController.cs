using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth = 10;

    public BossController bossController;
    // Start is called before the first frame update
    void Start()
    {   
        bossController = GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damaged(int amount) {
        currentHealth -= amount;

        if (currentHealth < 0) {
            currentHealth = 0;
        }

        BossHealthBar.Instance.setHealthPercent(currentHealth/maxHealth);

        if (currentHealth <= 0) {
            bossController.killed();
        } else {
            DamageBossCollectableSpawner.Instance.spawnNew();
        }
    }
}
