using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{   
    List<Skill> skills = new List<Skill>();

    public GameObject UIPrefab;

    public GameObject canvas;

    public Sprite sprite;

    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {   
        skills.Add(initSkill("test", 0));
        skills.Add(initSkill("test", 1));

        playerInput = new PlayerInput();
        playerInput.Player.Skill1.Enable();
        playerInput.Player.Skill2.Enable();

        playerInput.Player.Skill1.performed += Skill1Performed;
        playerInput.Player.Skill2.performed += Skill2Performed;
       
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
        UI.init(canvas, UIPrefab, sprite, index);
        Skill skill = gameObject.AddComponent<Skill>();
        skill.UI = UI;

        return skill;
    }
}
