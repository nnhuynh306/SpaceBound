using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossIdle : StateMachineBehaviour
{

    public float minIdleTime;
    public float maxIdleTime;

    private float idleTime;
    private float idleTimer;

    private bool finished = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer = 0;
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        finished = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime && !finished) {
            animator.SetTrigger("Move");
            finished = true;
        }
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
