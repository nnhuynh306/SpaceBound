using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantLevelPortal : Portal
{
    protected override void portalEnter()
    {
        GameManager.Instance.finishMerchantLevel();
    }
}
