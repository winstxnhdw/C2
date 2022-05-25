using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] CanvasGroup usernameCanvas;
    [SerializeField] CanvasGroup passwordCanvas;
    [SerializeField] CanvasGroup passwordVerificationCanvas;
    [SerializeField] CanvasGroup passwordRecallCanvas;
    [SerializeField] CanvasGroup recallVerificationCanvas;
    [SerializeField] CanvasGroup hackerDetectedCanvas;
    [SerializeField] CanvasGroup hackingCanvas;
    [SerializeField] CanvasGroup hackingVerificationCanvas;
    [SerializeField] CanvasGroup scoreCanvas;

    public static event Action initialiseLogs;

    enum GameStates {
        Username,
        Password,
        PasswordRecall,
        PasswordRecallVerification,
        HackerDetected,
        Hacking,
        HackingVerification,
        Scoring
    }

    CanvasGroup[] canvasGroups;
    GameStates gameState;

    void Awake() {
        this.gameState = GameStates.Username;
        this.canvasGroups = GetComponentsInChildren<CanvasGroup>(includeInactive: true);

        UsernameReply.onComplete += this.NextCanvas;
        PasswordReply.onComplete += this.EnablePasswordVerificationCanvas;
        PasswordVerification.onComplete += this.AfterPasswordVerification;
        RecallInstructions.onComplete += this.NextCanvas;
        RecallVerification.onComplete += this.NextCanvas;
        DetectedPrompt.onComplete += this.NextCanvas;
        HackingField.onComplete += this.NextCanvas;
        HackingVerification.onComplete += this.NextCanvas;
    }

    void Start() {
        this.SetActiveCanvases();
        GameManager.initialiseLogs?.Invoke();
    }

    void NextCanvas() {
        this.gameState++;
        this.SetActiveCanvases();
    }

    void EnablePasswordVerificationCanvas() {
        this.DisableAllCanvases();
        this.passwordVerificationCanvas.gameObject.SetActive(true);
    }

    void AfterPasswordVerification() {
        if (Player.PasswordCount == Settings.PasswordOrdinalIndicators.Count) this.gameState++;
        this.SetActiveCanvases();
    }

    void SetActiveCanvases() {
        this.DisableAllCanvases();

        switch (this.gameState) {
            case GameStates.Username:
                this.SetActiveCanvas(this.usernameCanvas);
                break;

            case GameStates.Password:
                this.SetActiveCanvas(this.passwordCanvas);
                break;

            case GameStates.PasswordRecall:
                this.SetActiveCanvas(this.passwordRecallCanvas);
                break;

            case GameStates.PasswordRecallVerification:
                this.SetActiveCanvas(this.recallVerificationCanvas);
                break;

            case GameStates.HackerDetected:
                this.SetActiveCanvas(this.hackerDetectedCanvas);
                break;

            case GameStates.Hacking:
                this.SetActiveCanvas(this.hackingCanvas);
                break;

            case GameStates.HackingVerification:
                this.SetActiveCanvas(this.hackingVerificationCanvas);
                break;

            case GameStates.Scoring:
                this.SetActiveCanvas(this.scoreCanvas);
                break;

            default:
                throw new Exception("Invalid game state");
        }
    }

    void DisableAllCanvases() {
        foreach (CanvasGroup canvas in this.canvasGroups) canvas.gameObject.SetActive(false);
    }

    void SetActiveCanvas(in CanvasGroup canvas) {
        canvas.gameObject.SetActive(true);
    }

    void OnDestroy() {
        UsernameReply.onComplete -= this.NextCanvas;
        PasswordReply.onComplete -= this.EnablePasswordVerificationCanvas;
        PasswordVerification.onComplete -= this.AfterPasswordVerification;
        RecallInstructions.onComplete -= this.NextCanvas;
        RecallVerification.onComplete -= this.NextCanvas;
        DetectedPrompt.onComplete -= this.NextCanvas;
        HackingField.onComplete -= this.NextCanvas;
        HackingVerification.onComplete -= this.NextCanvas;
    }
}