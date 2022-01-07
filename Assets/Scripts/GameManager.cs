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

    private GameObject chooseAvatarMenu;
    // Start is called before the first frame update
    void Start()
    {
        state = State.PLAYING;
        checkLevelSettings();
        checkLevelTheme();
    }

    void checkLevelSettings() {
        if (SceneManager.GetActiveScene().name.ToLower().StartsWith("level") || SceneManager.GetActiveScene().name.Equals("SampleScene")
         || SceneManager.GetActiveScene().name.Equals("MerchantLevel")) {
            playerInput = new PlayerInput();
            playerInput.Player.Pause.Enable();

            playerInput.Player.Pause.performed += OnPausePerformed;

            closePauseMenu();
        } else {
        

        }
    }

    void checkLevelTheme() {
        if (SceneManager.GetActiveScene().name.ToLower().StartsWith("level") || SceneManager.GetActiveScene().name.Equals("SampleScene")) {
            AudioManager.Instance.playOneAtATime("InGameTheme");
        } else if (SceneManager.GetActiveScene().name.Equals("MerchantLevel")) {
            AudioManager.Instance.playOneAtATime("MerchantLevelTheme");
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
        if (pauseMenu == null) {
            pauseMenu = createMenu("Prefabs/Menu/PauseMenu");
        }
    }

    public void pauseGame() {
        state = State.PAUSED;
        Time.timeScale = 0;
        FindObjectOfType<PlayerMovementController>().disableMovement();
    }

    public void continueGame() {
        state = State.PLAYING;
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
        createMenu("Prefabs/Menu/VictoryUI");

        AudioManager.Instance.playOneAtATime("Victory");
    }

    private void showDefeatScreen() {
        createMenu("Prefabs/Menu/GameOverMenu");

        AudioManager.Instance.playOneAtATime("Defeat");
    }

    private GameObject createMenu(string path) {
        GameObject menu = Instantiate(Resources.Load<GameObject>(path), Vector2.zero, Quaternion.identity);
        menu.transform.SetParent(GameObject.FindGameObjectWithTag("In-game UI").transform, false);

        return menu;
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
        if (chooseAvatarMenu == null) {
            chooseAvatarMenu = createMenu("Prefabs/Menu/ChooseAvatarMenu");
        } else {
            chooseAvatarMenu.SetActive(true);
        }
    }

    public void closeAvatarShop() {
        chooseAvatarMenu.SetActive(false);
        
        FindObjectOfType<PlayerAnimationController>().loadAvatar();
        StatUI.Instance.setAvatar();
    }

    public void finishMerchantLevel() {
        Invoke("finishMerchantLevelMethod", 2f);
        FindObjectOfType<PlayerController>().finish();
    }

    public void finishMerchantLevelMethod() {
        SceneManager.LoadScene(PlayerPrefs.GetString(PlayerPrefsKeys.CURRENT_LEVEL, "SampleScene"));
    }

    enum State {
        IS_FINISHING,
        GAME_OVER,
        PLAYING,
        PAUSED
    }
}
