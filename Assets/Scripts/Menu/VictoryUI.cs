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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCoin() {
        gameObject.transform.Find("CoinText").GetComponent<TextMeshProUGUI>().text = getCoinCollected();
    }

    string getCoinCollected() {
        return "2";
    }
    public void replay() {
        GameManager.Instance.replay();
    }
}
