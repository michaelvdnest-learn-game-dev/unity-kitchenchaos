using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    [SerializeField] private float footstepTimerMax = 0.1f;
    [SerializeField] private float footstepVolume = 1.0f;

    private float footstepTimer;
    private void Update() {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f ) {
            footstepTimer = footstepTimerMax;

            if (Player.Instance.IsWalking()) {
                SoundManager.Instance.PlayFootStepsSound(Player.Instance.transform.position, footstepVolume);
            }
        }
    }
}
