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

    private bool isKnockingBack = false;

    private float knockbackForce = 10;

    private float knockBackTimer = 0f;

    private float knockBackTime = 1f;

    public float recoverTimeAfterKnockback = 1f;

    private bool movementDisabled = false;
    
    private bool MovementDisabled {
        get {
            return movementDisabled;
        }
        set {
            movementDisabled = value;
        }
    }

    private SpriteBlinkingController spriteBlinkingController;

    public string[] lethalLayers = {"Enemy"};

    private PlayerAudioController playerAudioController;

    private PlayerController playerController;

    private bool higherJumpActivated = false;

    private float higherJumpMultiplier = 1;
    private GameObject higherJumpEffect;

    private float higherSpeedMultiplier = 1f;
    private GameObject higherSpeedEffect;

    public ParticleSystem jumpChargeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        animationController = GetComponent<PlayerAnimationController>();

        headCollider = transform.Find("HeadCollider").GetComponent<BoxCollider2D>();

        bodyCollider = transform.Find("BodyCollider").GetComponent<CircleCollider2D>();

        spriteBlinkingController = GetComponent<SpriteBlinkingController>();

        playerAudioController = GetComponent<PlayerAudioController>();

        playerController = GetComponent<PlayerController>();

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

        knockbackCheck();
    }

    void updateYVelocity() {
        animationController.updateYVelocity(rigidBody.velocity.y);
    }

    void knockbackCheck() {
        if (isKnockingBack) {
            knockBackTimer += Time.deltaTime;

            if (knockBackTimer > knockBackTime) {
                recoverAfterKnockback();
            }
        }
    }

    private void FixedUpdate() {
        move(Time.fixedDeltaTime);
    }

    private void move(float deltaTime) {
        userXAxisInput = playerInput.Player.Movement.ReadValue<float>();

        Vector2 position = transform.position;
        position.x += userXAxisInput * (isCrouching? crounchingRunSpeed: runSpeed) * deltaTime * higherSpeedMultiplier;
        
        moveAnimationCheck();
        moveAudioCheck();
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

    private void moveAudioCheck() {
        if (Mathf.Abs(userXAxisInput) > 0 && !isJumping) {
            if (isCrouching) {
                playerAudioController.crouchRunning();
            } else {
                playerAudioController.running();
            }
        } else {
            playerAudioController.stopRunning();
        }
    }

    //-- INPUT SYSTEM ---------------------------------------------------------------------------- //

    void JumpEnter(InputAction.CallbackContext context) {
        this.jumpStartTime = context.time;
        this.jumpChargeEffect.Play();
    }

    void JumpExit(InputAction.CallbackContext context) {
        jump(calculateForce(jumpStartTime, context.time));
        this.jumpChargeEffect.Stop();
    }

    float calculateForce(double startTime, double stopTime) {
        float totalChargeTime = (float)((stopTime - startTime) / jumpChargeTimeDenominator) + baseJumpChargeTime;
        float time = Mathf.Min(jumpChargeTimeMax, totalChargeTime);
        return time * (isCrouching? crounchingBaseJumpForce: baseJumpForce)
            * (higherJumpActivated? higherJumpMultiplier: 1);
    }

    float calculateBaseForce() {
         return baseJumpChargeTime * (isCrouching? crounchingBaseJumpForce: baseJumpForce);
    }

    private void CrouchEnter(InputAction.CallbackContext context) {
        crouch();
    }

    private void CrouchExit(InputAction.CallbackContext context) {
        standUp();
    }

    //-- MOVEMENT ---------------------------------------------------------------------------- //

    public void grounded() {
        isJumping = false;
        playerAudioController.grounded();
        animationController.grounded();
    }

    private void recoverAfterKnockback() {
        playerController.vincible();

        isKnockingBack = false;
        knockBackTimer = 0;

        enableMovement();
        animationController.stopHurt();
        Invoke("enableLethalInteraction", recoverTimeAfterKnockback);
    }

    public void jump(float jumpForce) {
        if (MovementDisabled) {
            return;
        }
        if (!isJumping) {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            isJumping = true;
            animationController.jumping();
            playerAudioController.jump();
            
            if (higherJumpActivated) {
                deactivateHigherJump();
            }
        }
    }

    public void jumpWithoutCharge() {
       jump(calculateBaseForce());
    } 

    public void crouch() {
        if (MovementDisabled) {
            return;
        }
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
    

    void flip() {
        facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    public void knockback(GameObject other) {

        playerController.invincible();

        applyKnockbackForce(other);

        isKnockingBack = true;

        disableMovement();
        animationController.hurt();
        disableLethalInteraction();
    }

    public void applyKnockbackForce(GameObject other) {
        Vector2 difference = (transform.position - other.transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    public void leaveGround() {
        isJumping = true;
    }

    //--- ON TRIGGER ---------------------------------------------------------------------------- //

    public void OnTriggerEnterGround() {
        canStandUp = false;
    }

    public void OnTriggerStayGround() {
        canStandUp = false;
    }

    public void OnTriggerExitGround() {
        if (isCrouching) {
            canStandUp = true;
            if (playerInput.Player.Crouch.phase == InputActionPhase.Waiting) {
                standUp();
            }
        }
    }

    //---- OTHER ---------------------------------------------------------------------------- //

    private void disableMovement() {
        MovementDisabled = true;
        playerInput.Player.Disable();
    }

    public void enableMovement() {
        MovementDisabled = false;
        playerInput.Player.Enable();
    }

    public void disableLethalInteraction() {
        foreach(string layerName in lethalLayers) {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer(layerName), true);
        }
        spriteBlinkingController.startBlinking();
    }

    public void enableLethalInteraction() {
         foreach(string layerName in lethalLayers) {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer(layerName), false);
        }
        spriteBlinkingController.stopBlinking();
    }

    //----MOVEMENT SKILLS---------------------------------------------------------------------------------//
    public void higherJump(float multiplier) {
        higherJumpActivated = true;
        higherJumpMultiplier = multiplier;
        higherJumpEffect = Instantiate(Resources.Load<GameObject>("Effects/HigherJumpEffect"), new Vector2(-0.07f, -0.9f), Quaternion.identity);
        higherJumpEffect.transform.SetParent(this.gameObject.transform, false);
    }

    private void deactivateHigherJump() {
        higherJumpActivated = false;
        Destroy(higherJumpEffect);
    }

    public void higherSpeed(float multiplier, float time) {
        higherSpeedMultiplier = multiplier;
        higherSpeedEffect = Instantiate(Resources.Load<GameObject>("Effects/HigherSpeedEffect"), new Vector2(-0.07f, -0.4f), Quaternion.identity);
        higherSpeedEffect.transform.SetParent(this.gameObject.transform, false);

        Invoke("deactivateHigherSpeed", time);
    }

    public void deactivateHigherSpeed() {
        higherSpeedMultiplier = 1f;
        Destroy(higherSpeedEffect);
    }
}
