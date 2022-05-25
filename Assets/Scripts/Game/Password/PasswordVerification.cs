using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordVerification : MonoBehaviour {
    [Header("Parameters")]
    [TextArea][SerializeField] string inputFieldText;
    [TextArea][SerializeField] string instructionsFieldText;
    [SerializeField] float animationDuration;

    [Header("Game Objects")]
    [SerializeField] TMP_InputField verifyField;
    [SerializeField] TextMeshProUGUI instructionsTextMesh;

    public delegate void ActionIn<Dictionary>(in Dictionary<string, bool> passwordValidity);
    public static event ActionIn<Dictionary<string, bool>> beginVerification;
    public static event Action onComplete;
    public static event Action onSuccessfulValidation;

    bool isVerifying;
    bool isPasswordValid;

    void Awake() {
        InputListener.onEnterPress += this.CompleteVerification;
        FakeLogs.onVerificationComplete += this.UpdateText;
        this.isVerifying = true;
    }

    void OnEnable() {
        this.verifyField.interactable = true;
        this.instructionsTextMesh.text = instructionsFieldText;
        this.OnEnableAnimation();

        Dictionary<string, bool> passwordRequirements = this.VerifyPassword();
        this.isPasswordValid = !passwordRequirements.Values.Contains(false);
        PasswordVerification.beginVerification?.Invoke(passwordRequirements);
    }

    void OnEnableAnimation() {
        this.verifyField.text = this.inputFieldText;
        Typewriter.AnimateLetters(this.verifyField, this.animationDuration);
    }

    void CompleteVerification() {
        if (this.isVerifying) return;
        this.isVerifying = true;
        PasswordVerification.onComplete?.Invoke();
    }

    Dictionary<string, bool> VerifyPassword() {
        // We assume that the most recent password is the string to verify
        IEnumerable<string> passwords = Player.Passwords;
        string password = passwords.Last();

        bool isPasswordLengthValid = password.Length >= Settings.minPasswordLength;
        bool isPasswordSymbolic = password.Length - password.Count(char.IsLetterOrDigit) > 0;
        bool isPasswordCaseValid = password.Any(char.IsUpper) && password.Any(char.IsLower);
        bool isPasswordAlphanumeric = password.Any(char.IsLetter) && password.Any(char.IsNumber);
        bool isUnique = !passwords.Take(passwords.Count() - 1).Contains(password);

        Dictionary<string, bool> passwordRequirements = new Dictionary<string, bool>();
        passwordRequirements.Add("Contains at least eight characters", isPasswordLengthValid);
        passwordRequirements.Add("Contains symbols", isPasswordSymbolic);
        passwordRequirements.Add("Contains upper and lower casings", isPasswordCaseValid);
        passwordRequirements.Add("Is alphanumeric", isPasswordAlphanumeric);
        passwordRequirements.Add("Is unique", isUnique);

        return passwordRequirements;
    }

    void UpdateText() {
        if (this.isPasswordValid) {
            PasswordVerification.onSuccessfulValidation?.Invoke();
            this.verifyField.text = "Success!";
            this.instructionsTextMesh.text = "Your password has met all the requirements.";
        }

        else {
            this.verifyField.text = "Failure!";
            this.instructionsTextMesh.text = "Your password has failed to meet one or more of the requirements. Try again.";
            Player.RemoveLastPassword();
        }

        Typewriter.AnimateLetters(this.verifyField, this.animationDuration);
        this.isVerifying = false;
    }

    void OnDestroy() {
        InputListener.onEnterPress -= this.CompleteVerification;
        FakeLogs.onVerificationComplete -= this.UpdateText;
    }
}