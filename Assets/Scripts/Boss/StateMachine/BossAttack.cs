using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    BossAttackController bossAttackController;

    public string bossAttackAuraPath = "Boss/BossAttackAura";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        bossAttackController = animator.gameObject.GetComponent<BossAttackController>();
        
        bossAttackController.attackAfter(1);

        AudioManager.Instance.play("BossSpellCasting");

        Instantiate(Resources.Load<GameObject>(bossAttackAuraPath), Vector3.zero, Quaternion.identity).transform.SetParent(animator.gameObject.transform, false);
    }
}
