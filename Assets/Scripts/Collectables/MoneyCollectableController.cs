 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectableController : CollectableController
{
    int amount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void applyOnPlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().collectMoney(amount);
    }
}
