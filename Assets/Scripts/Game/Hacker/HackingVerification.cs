using System;
using UnityEngine;
using TMPro;

public class HackingVerification : MonoBehaviour {
    [SerializeField] TMP_InputField verifyField;
    [SerializeField] TextMeshProUGUI instructionsTextMesh;

    public static event Action onComplete;
    public static event Action onHacked;
    public static event Action<bool> setContinueHUD;

    bool animationCompleted;

    void Awake() {
        InputListener.onEnterPress += this.CompleteVerification;
        this.animationCompleted = false;
    }

    void CompleteVerification() {
        if (!this.animationCompleted) return;
        this.animationCompleted = false;
        HackingVerification.onComplete?.Invoke();
    }

    void OnEnable() {
        if (Player.IsPasswordBruteforceable()) {
            this.PasswordHasBeenHacked();
        }

        else {
            this.PasswordHasNotBeenHacked();
        }
    }

    Typewriter AnimateWords() {
        return Typewriter.AnimateWords(this.verifyField, Settings.animationDelayBetweenWords);
    }

    void PasswordHasBeenHacked() {
        HackingVerification.onHacked?.Invoke();
        this.instructionsTextMesh.text = ColourChanger.SetErrorTextColour("SYSTEM COMPROMISED. GAME OVER!");
        this.verifyField.caretColor = Settings.errorTextColour;
        this.verifyField.GetComponentInChildren<TextMeshProUGUI>().color = Settings.errorTextColour;
        this.verifyField.text = "The hacker has infiltrated our systems as your password was weak and predictable.";
        this.AnimateWords()
            .SetOnComplete(() => {
                Sleep.BeforeFunction(() => {
                    this.verifyField.text = "SAF's C2 systems has been compromised, and our enemy has access to our sensitive information.";
                    this.AnimateWords();
                }, 2.0f).SetOnComplete(this.SetAnimationCompleted);
            });
    }

    void PasswordHasNotBeenHacked() {
        this.instructionsTextMesh.text = "SYSTEM SECURED. You may proceed to calculate your score.";
        this.verifyField.text = "The system has successfully warded off the hacking attempt!";
        this.AnimateWords()
            .SetOnComplete(this.SetAnimationCompleted);
    }

    void SetAnimationCompleted() {
        HackingVerification.setContinueHUD?.Invoke(true);
        this.animationCompleted = true;
    }

    void OnDestroy() {
        InputListener.onEnterPress -= this.CompleteVerification;
    }
}