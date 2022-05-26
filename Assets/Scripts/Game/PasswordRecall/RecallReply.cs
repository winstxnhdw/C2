using System;
using UnityEngine;
using TMPro;

public class RecallReply : MonoBehaviour {
    public static event Action onComplete;
    public static event Action<bool> setContinueHUD;
    TMP_InputField inputField;

    void Awake() {
        this.inputField = GetComponent<TMP_InputField>();
        this.inputField.onSubmit.AddListener(OnSubmit);
        this.inputField.onValueChanged.AddListener(OnValueChanged);

        RecallPrompt.onComplete += this.inputField.ActivateInputField;
    }

    void OnValueChanged(string text) {
        RecallReply.setContinueHUD?.Invoke(!string.IsNullOrWhiteSpace(text));
    }

    void OnSubmit(string recallAttempt) {
        if (this.inputField.wasCanceled) return;
        if (string.IsNullOrWhiteSpace(recallAttempt)) return;

        this.inputField.ActivateInputField();
        Player.SetRecalledPasswordAttempt(recallAttempt);
        RecallReply.setContinueHUD?.Invoke(false);
        RecallReply.onComplete?.Invoke();
    }

    void OnDestroy() {
        this.inputField.onSubmit.RemoveAllListeners();
        this.inputField.onValueChanged.RemoveAllListeners();

        RecallPrompt.onComplete -= this.inputField.ActivateInputField;
    }
}