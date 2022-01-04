using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Level_1");
    }

    public void SetName() {
       
    }

    public void LoadGame() { }

    public void OptionMenu() { }

    public void Exit() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Click() {
        animator.SetTrigger("Click");
    }


}
