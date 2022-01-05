using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{   
    List<Skill> skills = new List<Skill>();

    public GameObject UIPrefab;

    public GameObject canvas;

    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {   
        playerInput = new PlayerInput();
        playerInput.Player.Skill1.Enable();
        playerInput.Player.Skill2.Enable();

        playerInput.Player.Skill1.performed += Skill1Performed;
        playerInput.Player.Skill2.performed += Skill2Performed;

        skills.Add(initSkill("higherJump", 0));
        skills.Add(initSkill("Shield", 1));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill1Performed(InputAction.CallbackContext context) {
        skills[0].perform();
    }

    public void Skill2Performed(InputAction.CallbackContext context) {
        skills[1].perform();
    }

    private Skill initSkill(string name, int index) {
        SkillUI UI = gameObject.AddComponent<SkillUI>();
        UI.init(canvas, UIPrefab, index);
        Skill skill = gameObject.AddComponent<Skill>();
        skill.UI = UI;
        setSkillButtonText(UI, index);
        skill.parse(getSkillInfoBy(name));
        return skill;
    }

    private void setSkillButtonText(SkillUI skillUI, int index) {
        switch (index) {
            case 0: {
                skillUI.setButtonText(playerInput.Player.Skill1.GetBindingDisplayString());
                break;
            }
            case 1:
                skillUI.setButtonText(playerInput.Player.Skill2.GetBindingDisplayString());
                break;
            default: {
                skillUI.setButtonText(playerInput.Player.Skill1.GetBindingDisplayString());
                break;
            }
        }
    }

    private SkillInfo getSkillInfoBy(string name) {
        return SkillInfos.Instance.getBy(name);
    }

}
