using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherSpeedSkillPerformer : SkillPerformer
{
    public void perform(GameObject player) {
        player.GetComponent<PlayerMovementController>().higherSpeed(1.2f, 5);
    }
}
