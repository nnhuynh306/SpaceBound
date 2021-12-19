using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableController : MonoBehaviour
{
    public Animator animator;

    public string animationTriggerName = "Interacted";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void applyOnPlayer(GameObject player);
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            applyOnPlayer(other.gameObject);
            playAnimation();
        }
    }

    private void playAnimation() {
        animator.SetTrigger(animationTriggerName);
    }
}
