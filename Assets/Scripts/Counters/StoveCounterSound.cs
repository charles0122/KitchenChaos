using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    // ������Ч��ʱ��
    private float warningSoundTimer;
    float warningSoundTimerMax = .2f;

    // �Ƿ񲥷ž�����Ч
    private bool playWarningSound;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update() {

        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f) {
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
        
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = 0.5f;
         playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool playSoundFlag = e.state == StoveCounter.State.Frying || e.state ==  StoveCounter.State.Fried;
        if (playSoundFlag) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}
