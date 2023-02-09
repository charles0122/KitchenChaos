using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    private float footStepTimerMax = .1f;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footStepTimer-=Time.deltaTime;
        if (footStepTimer < 0) {
            if (player.IsWalking()) {
                footStepTimer = footStepTimerMax;
                SoundManager.Instance.PlayFootStepsSound(player.transform.position);
            }
        }
    }


}
