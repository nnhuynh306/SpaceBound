using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Image block;
    public int curLv;
    // Start is called before the first frame update
    public static void chooseLevel(int level)
    {
        GameManager.Instance.goToLevel(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Start()
    {
        if (curLv > PlayerPrefs.GetInt(PlayerPrefsKeys.MAX_PLAYER_LEVEL)) {
            block.gameObject.SetActive(true);
        }
    }
}
