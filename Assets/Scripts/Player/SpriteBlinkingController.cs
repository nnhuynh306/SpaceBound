using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlinkingController : MonoBehaviour
{
     public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public bool isBlinking = false;
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
    }

    private void SpriteBlinkingEffect()
    {      
        spriteBlinkingTimer += Time.deltaTime;
        if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
                this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
            } else {
                this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
            }
        }
    }
}
