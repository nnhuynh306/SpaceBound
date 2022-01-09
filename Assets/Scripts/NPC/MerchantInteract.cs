using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MerchantInteract : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public string isInteracting = null;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && currentInterObj)
        {
            isInteracting = "object is being interacted";
            TriggerDialogue();
        }
    }

    void OnTriggerEnter2D (Collider2D player)
    {
        if (player.CompareTag("Player's Body Collider"))
        {
            currentInterObj = player.gameObject;
        }
    }

    void OnTriggerExit2D (Collider2D player)
    {
        if (player.CompareTag("Player's Body Collider"))
        {
            if(player.gameObject == currentInterObj)
            {
                currentInterObj = null;
                isInteracting = null;
            }
        }
    }
}
