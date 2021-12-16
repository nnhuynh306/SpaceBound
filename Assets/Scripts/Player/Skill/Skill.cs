using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill: MonoBehaviour
{
    public SkillPerformer performer;

    public SkillUI UI;
    
    private float cooldownTime = 5;
    public float CooldownTime {
        set {
            if (value >= 0) {
                this.cooldownTime = value;
            }
        }
        get {
            return this.cooldownTime;
        }
    }
    
    private float timer = 0;

    private bool isCoolingDown = false;

    public Skill() {

    }

    public Skill(SkillPerformer performer, SkillUI UI) {
        this.performer = performer;
        this.UI = UI;
    }

    private void Update() {
        if (isCoolingDown) {
            if (timer > 0) {
                timer -= Time.deltaTime;
            } else{
                isCoolingDown = false;
            }
             UI.changeCooldownUI(timer, cooldownTime);
        }
    }

    public void perform() {
        if (!isCoolingDown) {
            // performer.perform();
            timer = cooldownTime;
            isCoolingDown = true;
            UI.changeCooldownUI(timer, cooldownTime);
        }
    }


    public void updateUI() {
        UI.changeCooldownUI(timer, cooldownTime);
    }

}
