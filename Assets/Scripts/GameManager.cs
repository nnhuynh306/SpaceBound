using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
     enum State {
        IS_FINISHING,
        GAME_OVER,
        PLAYING,
        PAUSED
    }
    PlayerInput playerInput;

    State state;

    private GameObject pauseMenu;

    private GameObject chooseAvatarMenu;

    private GameObject settingMenu;
    // Start is called before the first frame update
    void Start()
    {
        state = State.PLAYING;
        checkLevelTheme();
        checkLevelSettings();
    
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
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_LEVEL, 1) % 5 == 0) {
                AudioManager.Instance.playOneAtATime("BossTheme");
            } else {
                AudioManager.Instance.playOneAtATime("InGameTheme");
            }
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
        if (state == State.PAUSED) {
                continueGame();
                closePauseMenu();
            
        } else if (state == State.PLAYING) {
                pauseGame();
                openPauseMenu();
        }
    }

    void openPauseMenu() {
        if (pauseMenu == null) {
            findPauseMenu();
        }
        pauseMenu.SetActive(true);
    }

    public void closePauseMenu() {
        if (pauseMenu == null) {
            findPauseMenu();
        }
        pauseMenu.SetActive(false);
    }

    void findPauseMenu() {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu");
        if (pauseMenu == null) {
            Debug.Log("create Pause");
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

    public void victory() {
       if (state == State.PLAYING) {
            state = State.IS_FINISHING;

            checkMaxPlayerLevel();

            Invoke("showVictoryScreen", 1);

            FindObjectOfType<PlayerController>().finish();
       }
    }

    private void checkMaxPlayerLevel() {
        int currentLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_LEVEL, 1);
        if (currentLevel == PlayerPrefs.GetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL, 1)) {
            unlockNextLevel();
        }
    }

    private void unlockNextLevel() {
        int maxPlayerLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL, 1);
        if (maxPlayerLevel< GameConstants.MAX_POSSIBLE_LEVEL) {
            PlayerPrefs.SetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL, maxPlayerLevel + 1);
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

    public void addCoin(int amount) {
        if (amount <= 0) {
            return;
        }

        setPlayerCoint(amount + getPlayerCoin());
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
        int currentLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_LEVEL, 1);
        SceneManager.LoadScene("Level_" + currentLevel);
    }

    public void goToChooseLevelScene() {
        SceneManager.LoadScene("ChooseLevelMenu");
    }

    public void goToMerchantLevel() {
        SceneManager.LoadScene("MerchantLevel");
    }
    
    private void OnDestroy() {
        playerInput.Disable();
    }

    public void goToNextLevel() {
        int thisLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_LEVEL);
        int nextLevel = thisLevel + 1;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENT_LEVEL, nextLevel);

        goToMerchantLevel();
    }

    public void startNewGame(string name) {
        resetPlayerPrefs();

        PlayerPrefs.SetString(PlayerPrefsKeys.PLAYER_NAME, name);
        goToChooseLevelScene();
    }

    private void resetPlayerPrefs() {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.PLAYER_COIN, 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.CHARACTER_NAME, "fox");
    }

    private void setBasicPlayerRefs() {
        PlayerPrefs.SetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL, 1);
    }

    public void loadGame() {
        goToChooseLevelScene();
    }

    public void goToLevel(int level) {
        if (level <= 0 || level > PlayerPrefs.GetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL, 1)) {
            return;
        }

        PlayerPrefs.SetInt(PlayerPrefsKeys.CURRENT_LEVEL, level);
        goToMerchantLevel();

    }

    public void openSettingMenu() {
        settingMenu = createMenu("Prefabs/Option");
        createMenu("Prefabs/Settings");
    }

    public void closeSettingMenu() {
        settingMenu.SetActive(false);
    }

    public void stopTheme() {
        AudioManager.Instance.stop("InGameTheme");
        AudioManager.Instance.stop("BossTheme");
        AudioManager.Instance.stop("MenuTheme");
    }
}
