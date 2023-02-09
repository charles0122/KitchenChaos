using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;

    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;


    [SerializeField] private Button closeButton;

    private void Awake() {
        Instance = this;
        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => { Hide(); });
    }

    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: "+Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject?.SetActive(false);
    }
}
