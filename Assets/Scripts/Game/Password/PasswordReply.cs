using System;
using UnityEngine;
using TMPro;

public class PasswordReply : MonoBehaviour {
    public static event Action onComplete;
    public static event Action<bool> setContinueHUD;

    TMP_InputField inputField;

    void Awake() {
        this.inputField = GetComponent<TMP_InputField>();
        this.inputField.onSubmit.AddListener(OnSubmit);
        this.inputField.onValueChanged.AddListener(OnValueChanged);

        PasswordPrompt.onComplete += this.inputField.ActivateInputField;
    }

    void OnEnable() {
        this.inputField.text = string.Empty;
    }

    void OnValueChanged(string text) {
        PasswordReply.setContinueHUD?.Invoke(!string.IsNullOrWhiteSpace(text));
    }

    void OnSubmit(string password) {
        if (this.inputField.wasCanceled) return;
        if (string.IsNullOrWhiteSpace(password)) return;

        Player.AddPassword(password);
        PasswordReply.setContinueHUD?.Invoke(false);
        PasswordReply.onComplete?.Invoke();
    }

    void OnDestroy() {
        this.inputField.onSubmit.RemoveAllListeners();
        this.inputField.onValueChanged.RemoveAllListeners();

        PasswordPrompt.onComplete -= this.inputField.ActivateInputField;
    }
}