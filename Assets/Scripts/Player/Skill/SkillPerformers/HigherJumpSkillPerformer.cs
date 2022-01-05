using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherJumpSkillPerformer : SkillPerformer
{
 
    public void perform(GameObject player) {
        player.GetComponent<PlayerMovementController>().higherJump(1.2f);
    }
}
