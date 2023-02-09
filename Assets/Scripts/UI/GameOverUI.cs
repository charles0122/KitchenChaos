using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;
    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Show();
            recipeDeliveredText.text = DeliveryManager.Instance.GetSuccessFulRecipesAmount().ToString();
        } else {
            Hide();
        }
    }

    private void Update() {
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject?.SetActive(false);
    }
}