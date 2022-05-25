using System;

public class RecallPrompt : PromptField {
    public static event Action onComplete;

    protected override void OnEnable() {
        Player.SetChosenPasswordIndex(UnityEngine.Random.Range(0, Player.PasswordCount));
        this.promptField.text = StringHelpers.InterpolateFieldText(this.inputFieldText);

        base.OnEnable();
    }

    protected override void InvokeOnComplete() {
        RecallPrompt.onComplete?.Invoke();
    }
}