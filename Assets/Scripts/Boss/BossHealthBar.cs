using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : Singleton<BossHealthBar>
{
    float maxHealthBarLength;

    RectTransform maskRectTransform;
    private void Start() {
        maskRectTransform = transform.Find("Mask").GetComponent<RectTransform>();
        maxHealthBarLength = maskRectTransform.sizeDelta.x;
    }
    public void setHealthPercent(float percent) {
        maskRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHealthBarLength * percent);
    }
}
