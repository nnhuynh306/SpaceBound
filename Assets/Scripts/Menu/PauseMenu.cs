using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void continueGame() {
        GameManager.Instance.continueGame();
        GameManager.Instance.closePauseMenu();
    }

    public void replay() {
        GameManager.Instance.continueGame();        
        GameManager.Instance.replay();
    }

    public void merchant() {
        GameManager.Instance.continueGame();   
        GameManager.Instance.goToMerchantLevel();
    }

    public void setting() {

    }

    public void quit() {
        GameManager.Instance.continueGame();   
        GameManager.Instance.goToChooseLevelScene();
    }
}
