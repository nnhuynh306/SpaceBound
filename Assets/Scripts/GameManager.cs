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

            AudioManager.Instance.playOneAtATime("InGameTheme");

            closePauseMenu();
        } else {
            
            AudioManager.Instance.playOneAtATime("MenuTheme");
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
       if (state == State.PLAYING) {
            state = State.IS_FINISHING;
            Invoke("showVictoryScreen", 1);
            FindObjectOfType<PlayerController>().finish();
       }
    }

    public void defeated() {
        if (state != State.GAME_OVER) {
            state = State.GAME_OVER;
            Invoke("showDefeatScreen", 1);
            FindObjectOfType<PlayerController>().killed();
        }
    }

    private void showVictoryScreen() {
        GameObject victoryUI = Instantiate(Resources.Load<GameObject>("Prefabs/Menu/VictoryUI"), Vector2.zero, Quaternion.identity);
        victoryUI.transform.SetParent(GameObject.FindGameObjectWithTag("In-game UI").transform, false);

        AudioManager.Instance.playOneAtATime("Victory");
    }

    private void showDefeatScreen() {
        GameObject victoryUI = Instantiate(Resources.Load<GameObject>("Prefabs/Menu/GameOverMenu"), Vector2.zero, Quaternion.identity);
        victoryUI.transform.SetParent(GameObject.FindGameObjectWithTag("In-game UI").transform, false);

        AudioManager.Instance.playOneAtATime("Defeat");
    }

    public void replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void changeTheme() {

    }

    public int getPlayerCoin() {
        int currentCoint = PlayerPrefs.GetInt(PlayerPrefsKeys.PLAYER_COIN, -1);

        if (currentCoint == -1) {
            PlayerPrefs.SetInt(PlayerPrefsKeys.PLAYER_COIN, 0);
            return 0;
        }

        return currentCoint;
    }

    private void setPlayerCoint(int amount) {
        if (amount < 0) {
            return;
        }
        PlayerPrefs.SetInt(PlayerPrefsKeys.PLAYER_COIN, amount);
    }

    public bool spendCoin(int amount) {
        if (amount < 0) {
            return false;
        }

        int currentCoin = getPlayerCoin();

        if (currentCoin < amount) {
            return false;
        } else {
            setPlayerCoint(currentCoin - amount);
            return true;
        }

    }

    public void openAvatarShop() {
        SceneManager.LoadScene("ChooseAvatarMenu", LoadSceneMode.Additive);
    }

    public void closeAvatarShop() {
        SceneManager.UnloadSceneAsync("ChooseAvatarMenu");
        FindObjectOfType<PlayerAnimationController>().loadAvatar();
        StatUI.Instance.setAvatar();
    }

    enum State {
        IS_FINISHING,
        GAME_OVER,
        PLAYING,
        PAUSED
    }
}
