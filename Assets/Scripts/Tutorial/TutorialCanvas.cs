using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialCanvas : MonoBehaviour {
    [SerializeField] TMP_InputField textField;

    List<string> tutorialTexts;
    Typewriter typewriterObject;
    Keyboard currentKeyboard;
    int currentState;

    enum TutorialStates {
        Welcome,
        ExplanationOne,
        ExplanationTwo
    }

    void Awake() {
        this.currentKeyboard = Keyboard.current;
        this.currentState = 0;
        this.tutorialTexts = new List<string>() {
            "Welcome to the Singapore Armed Forces' Command & Control Systems!",
            "You have been registered as the commander of this system.",
            "As commander, you must secure our systems by registering three passwords.",
            "The three passwords must meet the following requirements."
        };
    }

    void Start() {
        this.DisplayTexts();
    }

    void Update() {
        if (currentKeyboard[Key.Enter].wasPressedThisFrame) {
            this.currentState++;
            Destroy(this.typewriterObject);
            this.DisplayTexts();
        }

        else if (currentKeyboard[Key.Escape].wasPressedThisFrame) {
            ChangeScene.IncrementScene();
        }
    }

    void DisplayTexts() {
        if (this.currentState >= this.tutorialTexts.Count) {
            ChangeScene.IncrementScene();
            return;
        }

        this.textField.text = this.tutorialTexts[this.currentState];
        this.typewriterObject = Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords)
                                          .SetOnComplete(() => {
                                              Sleep.BeforeFunction(this.DisplayTexts, 3.0f);
                                          });
        this.currentState++;
    }
}