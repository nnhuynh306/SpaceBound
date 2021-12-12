using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jumping() {
        animator.SetBool("Jumping", true);
                Debug.Log("jump");
    }

    public void grounded() {
        animator.SetBool("Jumping", false);
        Debug.Log("grounded");
    }

    public void setJumping(bool isJumping) {
        animator.SetBool("Jumping", isJumping);
    }

    public void updateYVelocity(float yVelocity) {
        animator.SetFloat("YVelocity", yVelocity);
    }

    public void run() {
        animator.SetFloat("Speed", 1);
    }

    public void idle() {
        animator.SetFloat("Speed", 0);
    }

    public void crouch() {
        animator.SetBool("Crouching", true);
    }

    public void standUp() {
        animator.SetBool("Crouching", false);
    }
}
