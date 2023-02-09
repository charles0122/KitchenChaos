using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private float volume = .3f;
    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1f) {
            volume = 0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume() {
        return volume;
    }
}