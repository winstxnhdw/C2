using System;

public class RecallPrompt : PromptField {
    public static event Action onComplete;

    protected override void OnEnable() {
        int passwordIndex = UnityEngine.Random.Range(0, Player.GetPasswordCount());
        string passwordIndexFormat = Player.GetPasswordOrdinalIndicator(passwordIndex);
        this.promptField.text = StringHelpers.InterpolateFieldText(this.inputFieldText)
                                             .Replace("{passwordIndex}", Player.GetPasswordOrdinalIndicator(passwordIndex));
        Player.SetChosenPasswordIndex(passwordIndex);

        base.OnEnable();
    }

    protected override void InvokeOnComplete() {
        RecallPrompt.onComplete?.Invoke();
    }
}