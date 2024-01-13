using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimer = 10f;

    private float gamePlayingTimerMax;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
        gamePlayingTimerMax = gamePlayingTimer;
    }

    private void Update() {
        float timer;
        switch (state) {
            case State.WaitingToStart:
                timer = waitingToStartTimer;
                timer -= Time.deltaTime;
                if (timer < 0f) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                waitingToStartTimer = timer;
                break;
            case State.CountdownToStart:
                timer = countdownToStartTimer;
                timer -= Time.deltaTime;
                if (timer < 0f) {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                countdownToStartTimer = timer;
                break;
            case State.GamePlaying:
                timer = gamePlayingTimer;
                timer -= Time.deltaTime;
                if (timer < 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                gamePlayingTimer = timer;
                break;
            case State.GameOver:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    public bool IsGamePlaying() {
       return (state == State.GamePlaying);
    }

    public bool IsCountdownToStartActive() {
        return (state == State.CountdownToStart);
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return (state  == State.GameOver);
    }

    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
