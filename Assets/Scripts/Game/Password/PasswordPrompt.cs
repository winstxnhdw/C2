using System;

public class PasswordPrompt : PromptField {
    public static event Action onComplete;

    protected override void OnEnable() {
        this.promptField.text = StringHelpers.InterpolateFieldText(this.inputFieldText);
        base.OnEnable();
    }

    protected override void InvokeOnComplete() {
        PasswordPrompt.onComplete?.Invoke();
    }
}