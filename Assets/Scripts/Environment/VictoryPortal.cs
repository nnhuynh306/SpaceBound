using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPortal : Portal
{
    protected override void portalEnter()
    {
        GameManager.Instance.finishGame();
    }
}
