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
        
        assignAvatar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void assignAvatar() {
        animator.runtimeAnimatorController = GetAnimatorController(PlayerPrefs.GetString(PlayerPrefsKeys.CHARACTER_NAME, "fox"));
    }

    private RuntimeAnimatorController GetAnimatorController(string name) {
        if (name == "bunny") {
            return Resources.Load<RuntimeAnimatorController>("Animation/PlayerBunny/PlayerBunnyAnimatorController");
        } else if (name == "squirrel") {
            return Resources.Load<RuntimeAnimatorController>("Animation/PlayerSquirrel/PlayerSquirrelAnimatorController");
        } else if (name == "fox") {
            return Resources.Load<RuntimeAnimatorController>("Animation/Player/PlayerAnimatorController");
        } else {
            return Resources.Load<RuntimeAnimatorController>("Animation/Player/PlayerAnimatorController");
        }
    }
    public void jumping() {
        animator.SetBool("Jumping", true);
    }

    public void grounded() {
        animator.SetBool("Jumping", false);
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

    public void hurt() {
        animator.SetBool("Hurt", true);
    }

    public void stopHurt() {
        animator.SetBool("Hurt", false);
    }
}
