using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;

    private bool isKilled = false;

    private Rigidbody2D rigidBody;

    public GameObject deathPrefab;

    public string deathSound = "EnemyDeath";

    public bool IsKilled {
        get {
            return isKilled;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killed() {
        isKilled = true;
        animator.SetTrigger("Death");
        disable();
    }

    private void disable() {
        AudioManager.Instance.play(deathSound);
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
