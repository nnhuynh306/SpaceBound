using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackAndForth : Movement
{
    public float speed = 4f;
    public float maxMoveTime = 2;
    float currentMoveTime = 0; 

    bool firstMove = false;

    public float firstDirectionX = -1;

    private Vector2 direction;

    private Animator animator;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        direction = new Vector2(firstDirectionX, 0).normalized;

    }
    private void FixedUpdate() {
        moveBackAndForth();
    
    }

    private void Update() {
        animationCheck();
    }


    void moveBackAndForth() {
        if (currentMoveTime > getMaxMoveTime()) {
            flipDirection();
            return;
        }

        Vector2 position = transform.position;

        position.x += speed * Time.fixedDeltaTime * direction.x;
        transform.position = position;

        currentMoveTime += Time.fixedDeltaTime;
    }

    void animationCheck() {
        if (direction == Vector2.left && facingRight) {
            flip();
        } else if (direction == Vector2.right && !facingRight) {
            flip();
        }
    }

    void flipDirection() {
        direction.x = -direction.x;
        if (firstMove) {
            firstMove = false;
        }
        currentMoveTime = 0;
    }

    float getMaxMoveTime() {
        if (firstMove) {
            return maxMoveTime / 2f;
        } else {
            return maxMoveTime;
        }
    }

    public override void disable() {
        this.enabled = false;
    }

    public override void enable()
    {
        this.enabled = true;
    }

}
