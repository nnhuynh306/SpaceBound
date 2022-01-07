using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBoxController : Singleton<DialogBoxController>
{
    TextMeshProUGUI dialogText;
    GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        dialogText = dialogBox.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hide() {
        dialogBox.SetActive(false);
    }

    public void show() {
        dialogBox.SetActive(true);
    }

    public void setText(string text) {
        dialogText.text = text;
    }
}
