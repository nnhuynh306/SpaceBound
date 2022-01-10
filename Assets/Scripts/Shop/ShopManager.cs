using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private Canvas CanvasObject;
    public GameManager manager;
    public Text money;
    public Transform skillTemplate;
    public Transform container;

    void Start()
    {
        CanvasObject = GameObject.Find("ShopCanvas").GetComponent<Canvas>();
        CanvasObject.GetComponent<Canvas>().enabled = false;
        
        foreach (var x in SkillInfos.Instance.skillInfos)
        {
            if(x.isBought == false)
            {
                Debug.Log(x.name);
                int index = SkillInfos.Instance.skillInfos.FindIndex(a => a.name == x.name);
                createSkillItem(x, index);
            }
        }
    }

    private void createSkillItem(SkillInfo skill, int index)
    {
        Transform skillTransform = Instantiate(skillTemplate, container);
        RectTransform skillRectTransform = skillTransform.GetComponent<RectTransform>();

        float skillWidth = 100f;
        skillRectTransform.anchoredPosition = new Vector2(skillWidth * index, 0);


        int s = skill.price;
        skillTransform.Find("PriceText").GetComponent<Text>().text = s.ToString();

        var sp = Resources.Load(skill.spritePath) as Sprite;
        skillTransform.Find("SkillSprite").GetComponent<Image>().sprite = sp;
    }

    void Update()
    {
        int currentCoin = manager.getPlayerCoin();
        money.text = currentCoin.ToString();
    }

    public void ToggleCanvas()
    {
        if (CanvasObject.enabled == true)
        {
            CanvasObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            CanvasObject.GetComponent<Canvas>().enabled = true;
        }
    }
}