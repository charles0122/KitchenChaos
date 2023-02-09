using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStart = 1f;
    private float countdownToStart = 3f;
    private float gamePlayingTimer = 10f;

    private void Awake() {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStart -= Time.deltaTime;
                if (waitingToStart < 0f) {
                    state = State.CountdownToStart; 
                }
                break;
            case State.CountdownToStart:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0f) {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.GameOver;
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }
}
