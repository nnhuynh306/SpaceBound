using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth = 10;
    // Start is called before the first frame update
    void Start()
    {
        BossHealthBar.Instance.setHealthPercent(currentHealth/maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damaged(int amount) {
        currentHealth -= amount;

        BossHealthBar.Instance.setHealthPercent(currentHealth/maxHealth);

    }
}
