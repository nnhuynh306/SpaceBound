using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : StateMachineBehaviour
{
     public float minmoveTime;
    public float maxmoveTime;

    private float moveTime;
    private float moveTimer;

    public float minMoveSpeed;
    public float maxMoveSpeed;

    private float moveSpeed;

    BossController bossController;

    private bool finished = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        finished = false;
        moveTimer = 0;
        moveTime = Random.Range(minmoveTime, maxmoveTime);
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        bossController = animator.gameObject.GetComponent<BossController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveTime) {
            if (!finished) {
                animator.SetTrigger("Attack");
                finished = true;
            }
        } else {
            bossController.moveTowardPlayer(moveSpeed);
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
