using System;

public class UsernamePrompt : PromptField {
    public static event Action onComplete;

    protected override void InvokeOnComplete() {
        UsernamePrompt.onComplete?.Invoke();
    }
}