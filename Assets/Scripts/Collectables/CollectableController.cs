using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableController : MonoBehaviour
{
    public Animator animator;

    public string animationTriggerName = "Interacted";

    public string collectedSound;

    public GameObject collectedEffectPrefab;

    public bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void applyOnPlayer(GameObject player);

    private void playAnimation() {
        Instantiate(collectedEffectPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void playAudio() {
        AudioManager.Instance.play(collectedSound);
    }

    public void collectedBy(GameObject gameObject) {
        if (!collected) {
             applyOnPlayer(gameObject);
            playAnimation();
            playAudio();
            collected = true;
        }
    }
}
