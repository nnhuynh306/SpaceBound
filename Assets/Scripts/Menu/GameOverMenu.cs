using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void replay() {
        GameManager.Instance.replay();
    }

    public void quit() {
        //to main menu
        GameManager.Instance.goToChooseLevelScene();
    }

    public void backToMerchant() {
        GameManager.Instance.goToMerchantLevel();
    }
}
