using System;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static event Action<Color> onError;
    public static event Action<bool> setContinue;

    void Awake() {
        UsernameReply.setContinueHUD += this.SetContinue;
        PasswordReply.setContinueHUD += this.SetContinue;
        FakeLogs.onVerificationComplete += this.ActivateContinue;
        PasswordVerification.onComplete += this.DeactivateContinue;
        RecallReply.setContinueHUD += this.SetContinue;
        HackingVerification.setContinueHUD += this.SetContinue;
        HackingVerification.onComplete += this.DeactivateContinue;
        CalculateScore.setContinueHUD += this.SetContinue;

        RecallVerification.onIncorrect += this.SetColoursToError;
        HackingVerification.onHacked += this.SetColoursToError;
    }

    void SetColoursToError() {
        UIManager.onError?.Invoke(Settings.ErrorTextColour);
    }

    void SetContinue(bool active) {
        UIManager.setContinue?.Invoke(active);
    }

    void ActivateContinue() {
        this.SetContinue(true);
    }

    void DeactivateContinue() {
        this.SetContinue(false);
    }

    void OnDestroy() {
        UsernameReply.setContinueHUD -= this.SetContinue;
        PasswordReply.setContinueHUD -= this.SetContinue;
        FakeLogs.onVerificationComplete -= this.ActivateContinue;
        PasswordVerification.onComplete -= this.DeactivateContinue;
        RecallReply.setContinueHUD -= this.SetContinue;
        HackingVerification.setContinueHUD -= this.SetContinue;
        HackingVerification.onComplete -= this.DeactivateContinue;
        CalculateScore.setContinueHUD -= this.SetContinue;

        RecallVerification.onIncorrect -= this.SetColoursToError;
        HackingVerification.onHacked -= this.SetColoursToError;
    }
}