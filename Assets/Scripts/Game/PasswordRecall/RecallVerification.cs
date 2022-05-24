using System;
using UnityEngine;
using TMPro;

public class RecallVerification : MonoBehaviour {
    [SerializeField] TMP_InputField verifyField;
    [SerializeField] TextMeshProUGUI instructionsTextMesh;
    [SerializeField] float delayBeforeNextScene;

    public static event Action onComplete;
    public static event Action onCorrect;
    public static event Action onIncorrect;

    void OnEnable() {
        if (Player.IsRecalledPasswordCorrect()) {
            this.OnCorrectRecall();
        }

        else {
            this.OnIncorrectRecall();
        }

        Typewriter.AnimateWords(this.verifyField, Settings.animationDelayBetweenWords);
    }

    void OnCorrectRecall() {
        this.instructionsTextMesh.text = "Success!";
        this.verifyField.text = "You have succesfully created a password to protect this system.";

        RecallVerification.onCorrect?.Invoke();
        Sleep.BeforeFunction(() => RecallVerification.onComplete?.Invoke(), this.delayBeforeNextScene);
    }

    void OnIncorrectRecall() {
        this.verifyField.caretColor = Settings.errorTextColour;
        this.verifyField.text = ColourChanger.SetErrorTextColour("The system is now inaccessible and a full system reset is underway.</color>");
        this.instructionsTextMesh.text = ColourChanger.SetErrorTextColour("PASSWORD INCORRECT. YOU HAVE BEEN LOCKED OUT.");

        RecallVerification.onIncorrect?.Invoke();
        Sleep.BeforeFunction(ChangeScene.RestartGame, this.delayBeforeNextScene);
    }
}