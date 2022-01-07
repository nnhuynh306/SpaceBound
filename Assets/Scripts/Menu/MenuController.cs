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
        SceneManager.LoadScene("SampleScene");
    }

    public void SetName() {
        SceneManager.LoadScene("ChooseName");
    }

    public void Shop() { 
        SceneManager.LoadScene("ShopUI");
    }

    public void LoadGame() { }

    public void OptionMenu() { }

    public void Cancel() {
        Application.Quit();
    }

    public void Click() {
        animator.SetTrigger("Click");
    }


}
