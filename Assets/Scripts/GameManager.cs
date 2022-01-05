using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    PlayerInput playerInput;

    State state;

    private GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        state = State.PLAYING;
        checkLevelMenu();
    }

    void checkLevelMenu() {
        if (SceneManager.GetActiveScene().name.ToLower().StartsWith("level") || SceneManager.GetActiveScene().name.Equals("SampleScene")) {
            playerInput = new PlayerInput();
            playerInput.Player.Pause.Enable();

            playerInput.Player.Pause.performed += OnPausePerformed;
            closePauseMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPausePerformed(InputAction.CallbackContext callbackContext) {
        if (Time.timeScale == 0) {
            if (state == State.PAUSED) {
                continueGame();
                closePauseMenu();
            }
        } else {
           if (state == State.PLAYING) {
                pauseGame();
                openPauseMenu();
           }
        }
    }

    void openPauseMenu() {
        if (pauseMenu == null) {
            findPauseMenu();
        }
        pauseMenu.SetActive(true);
    }

    void closePauseMenu() {
        if (pauseMenu == null) {
            findPauseMenu();
        }
        pauseMenu.SetActive(false);
    }

    void findPauseMenu() {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu");
    }

    public void pauseGame() {
        Time.timeScale = 0;
        FindObjectOfType<PlayerMovementController>().disableMovement();
    }

    public void continueGame() {
        Time.timeScale = 1;
        FindObjectOfType<PlayerMovementController>().enableMovement();
    }

    public void finishGame() {
       if (state != State.IS_FINISHING) {
            state = State.IS_FINISHING;
            Invoke("showVictoryScreen", 1);
            FindObjectOfType<PlayerController>().finish();
       }
    }

    private void showVictoryScreen() {
        GameObject victoryUI = Instantiate(Resources.Load<GameObject>("Prefabs/VictoryUI"), Vector2.zero, Quaternion.identity);
        victoryUI.transform.SetParent(GameObject.FindGameObjectWithTag("In-game UI").transform, false);
    }

    public void replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    enum State {
        IS_FINISHING,
        GAME_OVER,
        PLAYING,
        PAUSED
    }
}
