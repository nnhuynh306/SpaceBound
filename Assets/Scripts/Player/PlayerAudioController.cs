using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public string runningSound = "PlayerRunning";
    public string crounchRunningSound = "PlayerCrounchRunning";

    public string groundedSound = "Grounded";

    public string jumpSound = "PlayerJump";

    AudioManager audioManager;

    private void Start() {
        audioManager = AudioManager.Instance;
    }
    public void running() {
        audioManager.stop(crounchRunningSound);
        audioManager.playOneAtATime(runningSound);
    }

    public void stopRunning() {
        audioManager.stop(runningSound);
        audioManager.stop(crounchRunningSound);
    }

    public void crouchRunning() {
        audioManager.stop(runningSound);
        audioManager.playOneAtATime(crounchRunningSound);
    }

    public void grounded() {
        audioManager.play(groundedSound);
    }

    public void jump() {
        audioManager.play(jumpSound);
    }

    
}
