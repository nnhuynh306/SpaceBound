using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseAvatarMenu : MonoBehaviour
{
    string[] avatarNames = {"fox", "bunny", "squirrel"};
    int[] prices = {0, 200, 1000};

    int currentAvatarIndex = 0;

    Image avatarImage;

    GameObject applyButton;
    GameObject unlockButton;
    
    TextMeshProUGUI coinText;

    GameObject unlockCoin;
    TextMeshProUGUI unlockCoinText;
    

    // Start is called before the first frame update
    void Start()
    {
        avatarImage = transform.Find("Avatar").GetComponent<Image>();

        coinText = transform.Find("Coin").Find("CoinText").GetComponent<TextMeshProUGUI>();
        coinText.text = GameManager.Instance.getPlayerCoin().ToString();

        unlockCoin = transform.Find("UnlockCoin").gameObject;
        unlockCoinText = transform.Find("UnlockCoin").Find("CoinText").GetComponent<TextMeshProUGUI>();
        
        unlockButton = transform.Find("ButtonUnlock").gameObject;
        applyButton = transform.Find("ButtonOK").gameObject;

        setCurrentAvatar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextAvatar() {
        if (currentAvatarIndex < avatarNames.Length - 1) {
            currentAvatarIndex++;
        }

        setCurrentAvatar();
    }

    public void previousAvatar() {
        if (currentAvatarIndex > 0) {
            currentAvatarIndex--;
        }

        setCurrentAvatar();
    }

    void setCurrentAvatar() {
        avatarImage.sprite = Resources.Load<Sprite>("Sprite/Skins/" + avatarNames[currentAvatarIndex] + " image");

        bool unlocked = currentAvatarIsUnlocked();
        setButton(unlocked);

        setUnlockCoinText(unlocked);
    }

    void setButton(bool unlocked) {
        unlockButton.SetActive(!unlocked);
        applyButton.SetActive(unlocked && !currentAvatarIsApplied());
        Debug.Log(unlocked && !currentAvatarIsApplied());
    }

    bool currentAvatarIsApplied() {
        return PlayerPrefs.GetString(PlayerPrefsKeys.CHARACTER_NAME, "").Equals(avatarNames[currentAvatarIndex]);
    }

    void setUnlockCoinText(bool unlocked) {
        if (unlocked) {
            unlockCoin.SetActive(false);
        } else {
            unlockCoin.SetActive(true);
            unlockCoinText.text = prices[currentAvatarIndex].ToString();
        }
    }

    bool currentAvatarIsUnlocked() {
        int defaultUnlocked = currentAvatarIndex == 0? 1: 0;
        int unlockedInt = PlayerPrefs.GetInt(PlayerPrefsKeys.CHARACTER_UNLOCKED_PREFIX + avatarNames[currentAvatarIndex], defaultUnlocked);
       return Convert.ToBoolean(unlockedInt);
    }

    public void unlockCurrentAvatar() {
        if (currentAvatarIsUnlocked()) {
            return;
        }

        if (GameManager.Instance.spendCoin(prices[currentAvatarIndex])) {
            PlayerPrefs.SetInt(PlayerPrefsKeys.CHARACTER_UNLOCKED_PREFIX + avatarNames[currentAvatarIndex], 1);

            coinText.text = GameManager.Instance.getPlayerCoin().ToString();

            setCurrentAvatar();
        } else {
            unlockFail();
        }
    }

    public void applyAvatar() {
        PlayerPrefs.SetString(PlayerPrefsKeys.CHARACTER_NAME, avatarNames[currentAvatarIndex]);
        setCurrentAvatar();
    }

    void unlockFail() {
        
    }
}
