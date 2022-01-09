using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntro : StateMachineBehaviour {

    float time;

    GameObject boss;

    bool atMiddleOfEffect = false;
    bool exitIntro = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        boss = animator.gameObject;

        boss.transform.localScale = Vector3.zero;

        Instantiate(Resources.Load<GameObject>("Boss/BossIntroEffect"), boss.transform.position, Quaternion.identity);
        AudioManager.Instance.playOneAtATime("BossIntroStart");
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        time += Time.deltaTime;

        float scaleAmount = Time.deltaTime / 3f;

        if (time > 3.8 ) {
            boss.transform.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
            if (!atMiddleOfEffect) {
                AudioManager.Instance.playOneAtATime("BossIntroStop");
                atMiddleOfEffect = true;
            };
        }

        if (time > 6.8 && !exitIntro) {
            boss.transform.localScale = Vector3.one;
            animator.SetTrigger("Idle");
            DamageBossCollectableSpawner.Instance.spawnNew();
            exitIntro = true;
        }

    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Debug.Log(Time.time - time);
    }
}
