using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialCanvas : MonoBehaviour {
    [Header("Text Field")]
    [SerializeField] TMP_InputField textField;
    [SerializeField] float fieldTweenOffsetY;
    [SerializeField] float fieldTweenDuration;

    [Header("Logo Containers")]
    [SerializeField] GameObject AlphanumericContainer;
    [SerializeField] GameObject CasingContainer;
    [SerializeField] GameObject MemoryContainer;
    [SerializeField] GameObject SymbolsContainer;
    [SerializeField] GameObject LengthContainer;
    [SerializeField] float logoTweenOffsetY;
    [SerializeField] float logoTweenDuration;

    List<string> tutorialTexts;
    List<GameObject> GameObjects;
    Typewriter typewriterObject;
    Sleep sleepObject;
    Keyboard currentKeyboard;
    TutorialStates animatingIndex;
    int currentState;
    bool animatingState;
    bool complete;

    enum TutorialStates {
        Alphanumeric,
        Casing,
        Symbols,
        Length,
        Memory
    }

    void Awake() {
        this.complete = false;
        this.animatingIndex = 0;
        this.animatingState = false;
        this.currentKeyboard = Keyboard.current;
        this.currentState = 0;
        this.tutorialTexts = new List<string>() {
            "Welcome to the Singapore Armed Forces' Command & Control Systems!",
            "You have been registered as the commander of this system.",
            "As commander, you must secure our systems by registering three passwords.",
            "The three passwords must meet the following requirements."
        };

        this.GameObjects = new List<GameObject>() {
            this.AlphanumericContainer,
            this.CasingContainer,
            this.MemoryContainer,
            this.SymbolsContainer,
            this.LengthContainer
        };
    }

    void Start() {
        this.DisplayTexts();
    }

    void Update() {
        if (this.complete) {
            if (!currentKeyboard[Key.Enter].wasPressedThisFrame) return;
            ChangeScene.IncrementScene();
            return;
        }

        if (currentKeyboard[Key.Enter].wasPressedThisFrame) {
            this.currentState++;
            Destroy(this.typewriterObject);
            Destroy(this.sleepObject);
            if (!this.animatingState) this.DisplayTexts();

            else {
                this.AnimateLogos();
            }
        }

        else if (currentKeyboard[Key.Escape].wasPressedThisFrame) {
            ChangeScene.IncrementScene();
        }
    }

    void AnimateLogos() {
        switch (this.animatingIndex) {
            case TutorialStates.Alphanumeric:
                this.textField.text = "Your password must be alphanumeric.";
                this.TweenLogo(this.AlphanumericContainer);
                break;

            case TutorialStates.Casing:
                this.textField.text = "Your password must have upper and lower casings.";
                this.TweenLogo(this.CasingContainer);
                break;

            case TutorialStates.Symbols:
                this.textField.text = "Your password must contain symbols.";
                this.TweenLogo(this.SymbolsContainer);
                break;

            case TutorialStates.Length:
                this.textField.text = "Your password should be at least 8 characters long.";
                this.TweenLogo(this.LengthContainer);
                break;

            case TutorialStates.Memory:
                this.textField.text = "You must be able to remember your password.";
                this.TweenLogo(this.MemoryContainer);
                break;

            default:
                this.GameObjects.ForEach(x => x.SetActive(false));
                this.textField.text = "Ready? Press ENTER to begin!";
                this.textField.interactable = false;
                this.complete = true;
                this.TweenField(-1.0f);
                break;
        }

        this.animatingIndex++;
    }

    void TweenLogo(GameObject logo) {
        this.typewriterObject = this.AnimateWords();
        logo.SetActive(true);
        logo.LeanMoveLocalY(logo.transform.position.y + this.logoTweenOffsetY, this.logoTweenDuration).setEaseOutExpo();
    }

    void TweenField(float unitVector) {
        this.textField.gameObject.LeanMoveLocalY(this.textField.transform.position.y + (unitVector * this.fieldTweenOffsetY), this.fieldTweenDuration)
                                 .setEaseOutExpo();
    }

    Typewriter AnimateWords() {
        this.typewriterObject = Typewriter.AnimateWords(this.textField, Settings.AnimationDelayBetweenWords);
        return this.typewriterObject;
    }

    void DisplayTexts() {
        if (this.currentState >= this.tutorialTexts.Count) {
            this.animatingState = true;
            this.textField.interactable = false;
            this.TweenField(1.0f);
            this.AnimateLogos();
            return;
        }

        this.textField.text = this.tutorialTexts[this.currentState];
        this.AnimateWords().SetOnComplete(() => {
            this.sleepObject = Sleep.BeforeFunction(this.DisplayTexts, 4.0f);
            this.currentState++;
        });
    }
}