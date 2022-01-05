using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Pause.Enable();

        playerInput.Player.Pause.performed += OnPausePerformed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPausePerformed(InputAction.CallbackContext callbackContext) {
        if (Time.timeScale == 0) {
            continueGame();
        } else {
            pauseGame();
        }
    }

    public void pauseGame() {
        Time.timeScale = 0;
    }

    public void continueGame() {
        Time.timeScale = 1;
    }
}
