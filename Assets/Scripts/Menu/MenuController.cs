using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        String name;
        try {
            name = transform.Find("InputField").Find("Text").GetComponent<Text>().text;
        } catch (Exception e) {
            name = "";
        }
        GameManager.Instance.startNewGame(name);
    }

    public void SetName() {
       
    }

    public void LoadGame() {
        GameManager.Instance.loadGame();
    }

    public void OptionMenu() { }

    public void Exit() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Click() {
        animator.SetTrigger("Click");
    }


}
