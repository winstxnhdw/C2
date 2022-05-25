using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FakeLogs : MonoBehaviour {
    [SerializeField] TextMeshProUGUI fakeLogsText;
    public static event Action onVerificationComplete;

    string[] systemsLogTemplate;

    void Awake() {
        UIManager.onError += this.SetColoursToError;
        GameManager.initialiseLogs += this.InitialiseLogs;
        RecallVerification.onCorrect += this.SuccessLogs;
        PasswordVerification.beginVerification += this.SendVerificationLogs;

        this.systemsLogTemplate = new string[] {
            "WEAPONS SYSTEMS.... ",
            "BMS SYSTEMS.... ",
            "SURVEY SYSTEMS.... ",
            "NAVIGATION SYSTEMS.... ",
            "HUMS SYSTEMS.... ",
        };
    }

    void InitialiseLogs() {
        StartCoroutine(this.IInitaliseLogs());
    }

    IEnumerator IInitaliseLogs() {
        foreach (string log in systemsLogTemplate) {
            this.fakeLogsText.text += $"\n{log}initialising";
            yield return new WaitForSeconds(Settings.logAnimationDelay);
        }
    }

    void SuccessLogs() {
        StartCoroutine(this.ISuccessLogs());
    }

    IEnumerator ISuccessLogs() {
        foreach (string log in systemsLogTemplate) {
            this.fakeLogsText.text += $"\n{log}success";
            yield return new WaitForSeconds(Settings.logAnimationDelay);
        }
    }

    void SendVerificationLogs(in Dictionary<string, bool> passwordRequirements) {
        StartCoroutine(this.ISendVerificationLogs(passwordRequirements));
    }

    IEnumerator ISendVerificationLogs(Dictionary<string, bool> passwordRequirements) {
        this.fakeLogsText.text += $"\nVerifying the {Player.GetPasswordOrdinalIndicator(Player.PasswordCount - 1)} password....";

        foreach (KeyValuePair<string, bool> requirement in passwordRequirements) {
            yield return new WaitForSeconds(Settings.logAnimationDelay);
            this.fakeLogsText.text += $"\n{requirement.Key}.... {requirement.Value}";
        }

        FakeLogs.onVerificationComplete?.Invoke();
    }

    void SetColoursToError(Color errorTextColour) {
        this.fakeLogsText.color = ColourChanger.SetColourAlpha(errorTextColour, this.fakeLogsText.color.a);
    }

    void OnDestroy() {
        UIManager.onError -= this.SetColoursToError;
        GameManager.initialiseLogs -= this.InitialiseLogs;
        RecallVerification.onCorrect -= this.SuccessLogs;
        PasswordVerification.beginVerification -= this.SendVerificationLogs;
    }
}