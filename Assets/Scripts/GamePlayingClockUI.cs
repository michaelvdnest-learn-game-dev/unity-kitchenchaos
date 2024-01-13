using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour {

    [SerializeField] private Image timerImage;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        timerImage.fillAmount = 1f;
    }

    private void Update() {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
