using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatUI : Singleton<StatUI>
{
    private GameObject healthPrefab;
    private GameObject shieldPrefab;

    private float healthIconDistance = 15;

    private float shieldIconDistance = 15;

    private List<GameObject> healthIconArray = new List<GameObject>();

    private List<GameObject> shieldIconArray = new List<GameObject>();

    private Vector2 firstHealthIconPosition = new Vector2(68, -37);

    private Vector2 firstShieldIconPosition = new Vector2(68, -52);
    // Start is called before the first frame update
    void Start()
    {   
        setName();
        setAvatar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setName() {
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(PlayerPrefsKeys.PLAYER_NAME, "");
    }

    public void setAvatar() {
        transform.Find("CharacterImage").GetComponent<Image>().sprite 
            = getAvatar(PlayerPrefs.GetString(PlayerPrefsKeys.CHARACTER_NAME, "fox"));

    }

    private Sprite getAvatar(string name) {
        string skinPath = "Sprite/Skins/";
        return Resources.Load<Sprite>(skinPath + name + " image");
    }

    public void updateHealthUI(int health) {
        int healthDiff = health - healthIconArray.Count;
        if (healthDiff > 0) {
            increaseHealth(healthDiff);
        } else if (healthDiff < 0) {
            decreaseHealth(-healthDiff);
        }
    }

    void decreaseHealth(int amount) {
        if (amount > healthIconArray.Count) {
            amount = healthIconArray.Count;
        }

        for (int i = 0; i < amount; i++) {
            Destroy(healthIconArray[healthIconArray.Count - 1]);
            healthIconArray.RemoveAt(healthIconArray.Count - 1);
        }
    } 

    void increaseHealth(int amount) {
        for (int i = 0; i < amount; i++) {
            increase1Health();
        }
    }

    void increase1Health() {
        if (healthPrefab == null) {
            healthPrefab = Resources.Load<GameObject>("Prefabs/UI/Heart");
        }
        GameObject newHealth = Instantiate(healthPrefab, Vector2.zero, Quaternion.identity);
        newHealth.transform.SetParent(gameObject.transform, false);
        newHealth.GetComponent<RectTransform>().anchoredPosition = getNewHealthIconRectTransformPos();
        healthIconArray.Add(newHealth);
    }

    public void updateShieldUI(int shield) {
        int shieldDiff = shield - shieldIconArray.Count;
        if (shieldDiff > 0) {
            increaseShield(shieldDiff);
        } else if (shieldDiff < 0) {
            decreaseShield(-shieldDiff);
        }
    }

      void decreaseShield(int amount) {
        if (amount > shieldIconArray.Count) {
            amount = shieldIconArray.Count;
        }

        for (int i = 0; i < amount; i++) {
            Destroy(shieldIconArray[shieldIconArray.Count - 1]);
            shieldIconArray.RemoveAt(shieldIconArray.Count - 1);
        }
    } 

    void increaseShield(int amount) {
        for (int i = 0; i < amount; i++) {
            increase1Shield();
        }
    }

    void increase1Shield() {
        if (shieldPrefab == null) {
            shieldPrefab = Resources.Load<GameObject>("Prefabs/UI/Shield");
        }
        GameObject newShield = Instantiate(shieldPrefab, Vector2.zero, Quaternion.identity);
        newShield.transform.SetParent(gameObject.transform, false);
        newShield.GetComponent<RectTransform>().anchoredPosition = getNewShieldIconRectTransformPos();
        shieldIconArray.Add(newShield);
    }

    private Vector2 getNewHealthIconRectTransformPos() {
        return new Vector2(firstHealthIconPosition.x + healthIconDistance * healthIconArray.Count, firstHealthIconPosition.y);
    }

    private Vector2 getNewShieldIconRectTransformPos() {
        return new Vector2(firstShieldIconPosition.x + shieldIconDistance * shieldIconArray.Count, firstShieldIconPosition.y);
    }
}
