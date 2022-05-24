using System;

public class DetectedPrompt : PromptField {
    public static event Action onComplete;

    protected override void InvokeOnComplete() {
        Sleep.BeforeFunction(() => DetectedPrompt.onComplete?.Invoke(), Settings.hackerDetectedDelay);
    }
}