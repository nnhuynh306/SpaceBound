using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private int health = 1;
    private int shield = 3;

    private PlayerMovementController playerMovementController;

    public int Health {
        get {
            return health;
        }
        set {
            health = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StatUI.Instance.updateHealthUI(health);
        StatUI.Instance.updateShieldUI(shield);

        playerMovementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damaged(int amount) {
        if (amount <= 0) {
            return;
        }
        if (shield > 0) {
            if (amount >= shield) {
                amount -= shield;
                shield = 0;
                health -= amount;
            } else {
                shield -= amount;
            }
            updateShieldUI();
        } else {
            health -= amount;
        }
        updateHealthUI();
        
        if (health <= 0) {
            gameOver();
        }
    }

    public void increaseHealth(int amount) {
        if (amount <= 0) {
            return;
        }

        health += amount;
        updateHealthUI();

    }

    private void updateHealthUI() {
        StatUI.Instance.updateHealthUI(health);
    }

    private void updateShieldUI() {
        StatUI.Instance.updateShieldUI(shield);
    }

    public void gameOver() {
        GameManager.Instance.defeated();
    }

    public void instantDeath() {
        damaged(health + shield);
    }
}
