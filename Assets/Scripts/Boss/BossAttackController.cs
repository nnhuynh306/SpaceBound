using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public Vector2[] directions;

    public float projectileSpeed = 60f;

    GameObject bossProjectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        bossProjectilePrefab = Resources.Load<GameObject>("Boss/BossProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attackAfter(float time) {
        Invoke("attack", time);
    }

    public void attack() {
        for (int i = 0; i < directions.Length; i++) {
            GameObject bossProjectile = createProjectile(bossProjectilePrefab);

            bossProjectile.GetComponent<Rigidbody2D>().AddForce(directions[i] * projectileSpeed);
        }
    }

    private GameObject createProjectile(GameObject prefab) {
        return Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
