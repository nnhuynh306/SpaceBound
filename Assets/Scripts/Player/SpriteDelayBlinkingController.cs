using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDelayBlinkingController : MonoBehaviour
{
    private float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public bool isBlinking = false;

    public float accelerateMiniDuration = 0;

    public float accelerateDelayTime = 0;
    private  float accelerateDelayTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(isBlinking == true)
         { 
            SpriteBlinkingEffect();
         }
    }

    public void startBlinking() {
        isBlinking = true;
    }

    public void stopBlinking() {
        isBlinking = false;
        this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
        accelerateDelayTimer = 0;
        
    }

    private void SpriteBlinkingEffect()
    {      
        accelerateDelayTimer += Time.deltaTime;

        spriteBlinkingTimer += Time.deltaTime;
        if(canBlink())
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
                this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
            } else {
                this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
            }
        }
    }

    private bool canBlink() {
        if (accelerateDelayTimer >= accelerateDelayTime) {
            return spriteBlinkingTimer >= accelerateMiniDuration;
        } else {
            return spriteBlinkingTimer >= spriteBlinkingMiniDuration;
        }

    }
}
