using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBackAndForth : Movement
{
    public int jumpCountMax = 6;

    private int currentJumpCount = 0;

    public int jumpCountTilIdle = 2;

    public float jumpForce = 40;

    public Vector2 jumpDirection = new Vector2(-0.7f, 0.7f);

    public float idleTime = 1;
    float idleTimer = 0;

    private bool firstSwitchDirection = false;

    private Animator animator;

    public string jumpAnimationName = "YVelocity";

    public string jumpStateAnimationName = "Jumping";

    private Rigidbody2D rigidBody;


    private bool isGrounded = true;

    private bool isJumping = false;

    private Collider2D coll;

    public LayerMask groundLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        coll = GetComponent<Collider2D>();
    }

    private void Update() {
        animationCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(isGrounded) {
            nextMove();
        }
    }

    void nextMove() {
        if (currentJumpCount > getJumpCountMax()) {
            switchDirection();
            return;
        }

        if (jumpCountTilIdle > 0) {
            if (currentJumpCount % jumpCountTilIdle == 0) {
                idle();
                return;
            }
        }

        jump();
    }

    void jump() {
        if (isGrounded) {
            Debug.Log("Jump");
            rigidBody.AddForce(jumpForce * jumpDirection, ForceMode2D.Impulse);
            animator.SetBool(jumpStateAnimationName, true);
            currentJumpCount++;
            isGrounded = false;
        }
    }

    void idle() {
        if (idleTimer > idleTime) {
                currentJumpCount ++;
                idleTimer = 0;
        } else {
                idleTimer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            if (!isGrounded) { 
                isGrounded = true;
                animator.SetBool(jumpStateAnimationName, false);
                Debug.Log("grounded");
            }
        }
    }

    void animationCheck() {
        animator.SetFloat(jumpAnimationName, rigidBody.velocity.y);
    }
    

    void switchDirection() {
        jumpDirection.x = -jumpDirection.x;

        flip();

        if (!firstSwitchDirection) {
            firstSwitchDirection = true;
        }

        currentJumpCount = 0;
    }

    int getJumpCountMax() {
        if (firstSwitchDirection) {
            return jumpCountMax;
        } else {
            return jumpCountMax / 2;
        }
    }

    public override void disable() {

    }

    public override void enable() {

    }

}
