using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private float runSpeed = 5;
    private float crounchingRunSpeed = 2;
    private double jumpStartTime;

    private float baseJumpForce = 12.5f;

    private float crounchingBaseJumpForce = 10f;

    private float baseJumpChargeTime = 1f;

    private float jumpChargeTimeMax = 1.5f;

    //for faster or slower charge
    private float jumpChargeTimeDenominator = 1;

    private Rigidbody2D rigidBody;

    private PlayerInput playerInput;

    private float userXAxisInput;

    private bool facingRight = true;
    private bool isJumping = false;

    private bool isCrouching = false;

    private bool canStandUp = true;

    private PlayerAnimationController animationController;

    private BoxCollider2D headCollider;

    private CircleCollider2D bodyCollider;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        animationController = GetComponent<PlayerAnimationController>();

        headCollider = GetComponent<BoxCollider2D>();

        bodyCollider = GetComponent<CircleCollider2D>();

        assignInput();
    }

    void assignInput() {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        playerInput.Player.Jump.started += JumpEnter;
        playerInput.Player.Jump.canceled +=  JumpExit;

        playerInput.Player.Crouch.started += CrouchEnter;
        playerInput.Player.Crouch.canceled += CrouchExit;
    }

    // Update is called once per frame
    void Update()
    {
        animationController.updateYVelocity(rigidBody.velocity.y);
        updateYVelocity();
    }

    void updateYVelocity() {
        animationController.updateYVelocity(rigidBody.velocity.y);
    }

    private void FixedUpdate() {
        move(Time.fixedDeltaTime);
    }

    private void move(float deltaTime) {
        userXAxisInput = playerInput.Player.Movement.ReadValue<float>();

        Vector2 position = transform.position;
        position.x += userXAxisInput * (isCrouching? crounchingRunSpeed: runSpeed) * deltaTime;
        
        moveAnimationCheck();
        flipCheck();
        
        rigidBody.position = position;
    }

    private void moveAnimationCheck() {
        if (Mathf.Abs(userXAxisInput) == 0) {
            animationController.idle();
        } else {
            animationController.run();
        }
    }

    private void flipCheck() {
        if (facingRight && userXAxisInput < 0) {
            flip();
        } else if (!facingRight && userXAxisInput > 0) {
            flip();
        }
    }

    //-- INPUT SYSTEM ---------------------------------------------------------------------------- //

    void JumpEnter(InputAction.CallbackContext context) {
        this.jumpStartTime = context.time;
    }

    void JumpExit(InputAction.CallbackContext context) {
        jump(calculateForce(jumpStartTime, context.time));
    }

    float calculateForce(double startTime, double stopTime) {
        float totalChargeTime = (float)((stopTime - startTime) / jumpChargeTimeDenominator) + baseJumpChargeTime;
        float time = Mathf.Min(jumpChargeTimeMax, totalChargeTime);
        return time * (isCrouching? crounchingBaseJumpForce: baseJumpForce);
    }

    private void CrouchEnter(InputAction.CallbackContext context) {
        crouch();
    }

    private void CrouchExit(InputAction.CallbackContext context) {
        standUp();
    }

    void disableMovement() {

    }

    //-- MOVEMENT ---------------------------------------------------------------------------- //

    public void grounded() {
        isJumping = false;
        animationController.grounded();
    }

    public void jump(float jumpForce) {
        if (!isJumping) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            isJumping = true;
            animationController.jumping();
        }
    }

    public void crouch() {
        if (!isCrouching) {
            isCrouching = true;
            
            headCollider.isTrigger = true;
            animationController.crouch();
        }
    }

    public void standUp() {
        if (isCrouching && canStandUp) {
            isCrouching = false;

            headCollider.isTrigger = false;
            animationController.standUp();
        }
    }
    
    void toggleJumping() {
        isJumping = !isJumping;
        animationController.setJumping(isJumping);
    }

    void flip() {
        facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    //--- ON TRIGGER ---------------------------------------------------------------------------- //
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (detectChildTrigger(other)) {
            return;
        }

         if (other.gameObject.CompareTag("Ground")) {
            canStandUp = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (detectChildTrigger(other)) {
            return;
        }

         if (other.gameObject.CompareTag("Ground")) {
            canStandUp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (detectChildTrigger(other)) {
            return;
        }

        if (other.gameObject.CompareTag("Ground")) {
            if (isCrouching) {
                canStandUp = true;
                if (playerInput.Player.Crouch.phase == InputActionPhase.Waiting) {
                    standUp();
                }
            }
        }
    }


    private bool detectChildTrigger(Collider2D other) {
        if (other.gameObject.CompareTag("Player's Detector")) {
            return true;
        } else {
            return false;
        }
    }
}
