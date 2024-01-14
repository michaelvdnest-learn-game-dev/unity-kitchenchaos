using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipesDeliveredAmountText;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
        recipesDeliveredAmountText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
    }

    private void Hide() {
        gameObject.SetActive(false);

    }
}
