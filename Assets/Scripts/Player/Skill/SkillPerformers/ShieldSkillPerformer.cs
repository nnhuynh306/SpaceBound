using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkillPerformer : SkillPerformer
{

    public void perform(GameObject player) {
        player.GetComponent<PlayerController>().shield(5);
    }
}
