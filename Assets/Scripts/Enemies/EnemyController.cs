using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;

    private bool isKilled = false;

    public bool IsKilled {
        get {
            return isKilled;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killed() {
        Debug.Log("Kill enemy");
        isKilled = true;
        animator.SetTrigger("Death");
        DisableColliders();
    }

    public void DisableColliders()
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
        {  
            c.enabled = false;
        }
    }

}
