using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 1;
    public string explosionEffectPath = "Boss/BossProjectileHit";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroySelf() {
        Instantiate(Resources.Load<GameObject>(explosionEffectPath), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("DeathZone")) {
            Destroy(gameObject);
        }
    }
}
