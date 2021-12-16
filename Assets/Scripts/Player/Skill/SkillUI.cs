using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillUI: MonoBehaviour
{

    private Sprite sprite;

    private Image image;

    private Image cooldownImage;

    private TextMeshProUGUI cooldownText;

    private RectTransform rectTransform;

    private float distanceBetweenUI = 40;

    private float distanceToScreenBorder = 10;

    public void init(GameObject canvas, GameObject UIPrefab, Sprite sprite, int index) {
        GameObject container = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity, canvas.transform);

        rectTransform = container.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = calculateFinalRectTransformPos(index);

        Transform mask = container.transform.Find("Mask");
        image = mask.Find("Image").GetComponent<Image>();
        image.sprite = sprite;
        cooldownImage = mask.Find("Cooldown Mask").GetComponent<Image>();

        cooldownText = container.transform.Find("Cooldown Text").GetComponent<TextMeshProUGUI>();
    }

    public Vector2 calculateFinalRectTransformPos(int index) {
        float imageWidth = rectTransform.sizeDelta.x;

        float x = imageWidth/2 + distanceBetweenUI * (index + 1) + imageWidth * index;

        return new Vector2(x, distanceToScreenBorder + imageWidth/2);
    }

    public void changeCooldownUI(float timer, float maxCooldown) {
        float percent = timer/maxCooldown;
        cooldownImage.fillAmount = percent;
        
        if (timer > 0) {
            if (timer >= 1) {
                cooldownText.text = System.Math.Round(timer,0).ToString();
            } else {
                cooldownText.text = System.Math.Round(timer,1).ToString();
            }
        } else {
            cooldownText.text = "";
        }
    }
}
