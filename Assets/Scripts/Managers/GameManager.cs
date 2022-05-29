using System;
using System.Collections.Generic;
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
    Dictionary<GameStates, CanvasGroup> canvasGroupDictionary;
    GameStates gameState;

    void Awake() {
        UsernameReply.onComplete += this.NextCanvas;
        PasswordReply.onComplete += this.EnablePasswordVerificationCanvas;
        PasswordVerification.onComplete += this.AfterPasswordVerification;
        RecallInstructions.onComplete += this.NextCanvas;
        RecallVerification.onComplete += this.NextCanvas;
        DetectedPrompt.onComplete += this.NextCanvas;
        HackingField.onComplete += this.NextCanvas;
        HackingVerification.onComplete += this.NextCanvas;

        this.gameState = GameStates.Username;
        this.canvasGroups = GetComponentsInChildren<CanvasGroup>(includeInactive: true);
        this.canvasGroupDictionary = new Dictionary<GameStates, CanvasGroup>() {
            { GameStates.Username, this.usernameCanvas },
            { GameStates.Password, this.passwordCanvas },
            { GameStates.PasswordRecall, this.passwordRecallCanvas },
            { GameStates.PasswordRecallVerification, this.recallVerificationCanvas },
            { GameStates.HackerDetected, this.hackerDetectedCanvas },
            { GameStates.Hacking, this.hackingCanvas },
            { GameStates.HackingVerification, this.hackingVerificationCanvas },
            { GameStates.Scoring, this.scoreCanvas }
        };
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
        if (!canvasGroupDictionary.TryGetValue(this.gameState, out CanvasGroup canvasGroup)) throw new Exception("Invalid game state");
        this.SetActiveCanvas(canvasGroup);
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