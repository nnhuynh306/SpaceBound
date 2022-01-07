using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryUI : Singleton<VictoryUI>
{
    // Start is called before the first frame update
    void Start()
    {
        setCoin();
        GameManager.Instance.addCoin(getCoinCollected());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCoin() {
        gameObject.transform.Find("Coin").Find("CoinText").GetComponent<TextMeshProUGUI>().text = getCoinCollectedAsText();
    }

    string getCoinCollectedAsText() {
        return getCoinCollected().ToString();
    }

    int getCoinCollected() {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_LEVEL_MONEY, 0);
    }
    public void replay() {
        GameManager.Instance.replay();
    }

    public void nextLevel() {
        GameManager.Instance.goToNextLevel();
    }

    public void goToMainMenu() {
        GameManager.Instance.goToChooseLevelScene();
    }
}
