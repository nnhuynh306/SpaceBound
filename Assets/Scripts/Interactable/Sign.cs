using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    public string text;


    bool playerEntered = false;

    int interactTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if (!playerEntered && other.gameObject.CompareTag("Player's Body Collider")) {
            DialogBoxController.Instance.setText(text);
            DialogBoxController.Instance.show();
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (playerEntered && other.gameObject.CompareTag("Player's Body Collider")) {
            DialogBoxController.Instance.hide();
            playerEntered = false;
        }
    }
}
