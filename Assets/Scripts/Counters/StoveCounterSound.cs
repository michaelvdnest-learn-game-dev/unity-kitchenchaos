using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
         switch (e.state) {
            case StoveCounter.State.Idle:
            case StoveCounter.State.Burned:
                audioSource.Stop();
                break;
            case StoveCounter.State.Frying:
            case StoveCounter.State.Fried:
                audioSource.Play();
                break;
        };
    }
}
