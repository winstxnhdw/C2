public class OnCompleteCanvas : TemplateCanvas {
    void Awake() {
        TutorialManager.onCompleteTutorial += this.OnCompleteTutorial;
    }

    void OnCompleteTutorial() {
        this.inputField.text = this.inputFieldText;
        Typewriter.AnimateWords(this.inputField, Settings.animationDelayBetweenWords);
    }

    void OnDestroy() {
        TutorialManager.onCompleteTutorial += this.OnCompleteTutorial;
    }
}