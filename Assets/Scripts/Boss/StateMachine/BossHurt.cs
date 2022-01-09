using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : StateMachineBehaviour
{
    SpriteBlinkingController spriteBlinkingController;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        spriteBlinkingController = animator.gameObject.GetComponent<SpriteBlinkingController>();
        spriteBlinkingController.startBlinking();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        spriteBlinkingController.stopBlinking();
    }
}
