using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void StartGame() { }

    public void LoadGame() { }

    public void OptionMenu() { }

    public void Cancel() { }

    public void Click() {
        animator.SetTrigger("Click");
    }


}
