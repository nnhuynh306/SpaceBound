using System.Collections;
using System.Collections.Generic;
using System;

public class SkillInfos
{
    private static SkillInfos instance;

    public static SkillInfos Instance {
        get {
            if (instance == null) {
                instance = new SkillInfos();
            }
            return instance;
        }
    }

    private SkillInfos() {
        createData();
    }
    public List<SkillInfo> skillInfos = new List<SkillInfo>();

    private void createData() {
        SkillInfo higherJump = new SkillInfo();
        higherJump.spritePath = "Sprite/Skills/HigherJump";
        higherJump.name = "higherJump";
        higherJump.cooldownTime = 2;
        higherJump.skillPerformer = new HigherJumpSkillPerformer();

        SkillInfo shield = new SkillInfo();
        shield.spritePath = "Sprite/Skills/Shield";
        shield.name = "shield";
        shield.cooldownTime = 7;
        shield.skillPerformer = new ShieldSkillPerformer();

        skillInfos.Add(higherJump);
        skillInfos.Add(shield);
    }

    public SkillInfo getBy(string name) {
        return Array.Find<SkillInfo>(skillInfos.ToArray(), x => x.name.Equals(name));
    }
}
