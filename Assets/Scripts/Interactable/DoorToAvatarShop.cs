using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorToAvatarShop : Door
{

    protected override void interact(InputAction.CallbackContext context)
    {
        Debug.Log("interact");
        GameManager.Instance.openAvatarShop();
    }

}
