using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter counter;
    [SerializeField] private GameObject[] visualGameObjects;

    private void Start() {
        counter.OnStateChanged += Counter_OnStateChanged;
    }

    private void Counter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        switch (e.state) {
            case StoveCounter.State.Idle:
            case StoveCounter.State.Burned:
                Hide();
                break;
            case StoveCounter.State.Frying:
            case StoveCounter.State.Fried:
                Show();
                break;
        };
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        //if (e.selectedCounter == counter) {
        //    Show();
        //} else {
        //    Hide();
        //}
    }

    private void Show() {
        foreach (GameObject visualGameObject in visualGameObjects) {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide() {
         foreach (GameObject visualGameObject in visualGameObjects) {
            visualGameObject.SetActive(false);
        }
    }

}
