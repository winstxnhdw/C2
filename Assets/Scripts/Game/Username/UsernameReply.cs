using System;
using UnityEngine;
using TMPro;

public class UsernameReply : MonoBehaviour {
    public static event Action onComplete;
    public static event Action<bool> setContinueHUD;

    TMP_InputField inputField;

    void Awake() {
        this.inputField = GetComponent<TMP_InputField>();
        this.inputField.onSubmit.AddListener(OnSubmit);
        this.inputField.onValueChanged.AddListener(OnValueChanged);

        UsernamePrompt.onComplete += this.inputField.ActivateInputField;
    }

    void OnValueChanged(string text) {
        UsernameReply.setContinueHUD?.Invoke(!string.IsNullOrWhiteSpace(text));
    }

    void OnSubmit(string username) {
        if (this.inputField.wasCanceled) return;
        if (string.IsNullOrWhiteSpace(username)) return;

        Player.CreateUsername(username.Trim());
        UsernameReply.setContinueHUD?.Invoke(false);
        UsernameReply.onComplete?.Invoke();
    }

    void OnDestroy() {
        this.inputField.onSubmit.RemoveAllListeners();
        this.inputField.onValueChanged.RemoveAllListeners();

        UsernamePrompt.onComplete -= this.inputField.ActivateInputField;
    }
}