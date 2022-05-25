using System;
using UnityEngine;
using TMPro;

public class PasswordReply : MonoBehaviour {
    [SerializeField] TextMeshProUGUI instructionsTextMesh;
    [TextArea][SerializeField] string instructionsText;

    public static event Action onComplete;
    public static event Action<bool> setContinueHUD;

    TMP_InputField inputField;

    void Awake() {
        this.inputField = GetComponent<TMP_InputField>();
        this.inputField.onSubmit.AddListener(OnSubmit);
        this.inputField.onValueChanged.AddListener(OnValueChanged);
        this.instructionsTextMesh.text = this.instructionsText.Replace("{passwordCharacters}", this.inputField.characterLimit.ToString());

        PasswordPrompt.onComplete += this.inputField.ActivateInputField;
    }

    void OnEnable() {
        this.inputField.text = string.Empty;
    }

    void OnValueChanged(string text) {
        string remainingCharacters = (this.inputField.characterLimit - text.Length).ToString();
        this.instructionsTextMesh.text = this.instructionsText.Replace("{passwordCharacters}", remainingCharacters);
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