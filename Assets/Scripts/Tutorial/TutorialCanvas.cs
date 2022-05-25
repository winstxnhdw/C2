using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialCanvas : MonoBehaviour {
    [SerializeField] TMP_InputField textField;
    [SerializeField] TMP_InputField completeField;
    [SerializeField] GameObject AlphanumericContainer;
    [SerializeField] GameObject CasingContainer;
    [SerializeField] GameObject MemoryContainer;
    [SerializeField] GameObject SymbolsContainer;
    [SerializeField] GameObject LengthContainer;

    List<string> tutorialTexts;
    List<GameObject> GameObjects;
    Typewriter typewriterObject;
    Keyboard currentKeyboard;
    int currentState;
    TutorialStates animatingIndex;
    bool animatingState;
    Sleep sleepObject;
    bool complete;

    enum TutorialStates {
        Alphanumeric,
        Casing,
        Memory,
        Symbols,
        Length
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
                switch (this.animatingIndex) {
                    case TutorialStates.Alphanumeric:
                        this.AlphanumericContainer.SetActive(true);
                        this.textField.text = "Your password must be alphanumeric.";
                        Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords);
                        this.AlphanumericContainer.LeanMoveLocalY(this.AlphanumericContainer.transform.position.y + 0.15f, 0.75f).setEaseOutExpo();
                        break;

                    case TutorialStates.Casing:
                        this.CasingContainer.SetActive(true);
                        this.textField.text = "Your password must have lower and upper casings.";
                        Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords);
                        this.CasingContainer.LeanMoveLocalY(this.CasingContainer.transform.position.y + 0.15f, 0.75f).setEaseOutExpo();
                        break;

                    case TutorialStates.Memory:
                        this.MemoryContainer.SetActive(true);
                        this.textField.text = "You must be able to remember your password.";
                        Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords);
                        this.MemoryContainer.LeanMoveLocalY(this.MemoryContainer.transform.position.y + 0.15f, 0.75f).setEaseOutExpo();
                        break;

                    case TutorialStates.Symbols:
                        this.SymbolsContainer.SetActive(true);
                        this.textField.text = "Your password must contain symbols.";
                        Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords);
                        this.SymbolsContainer.LeanMoveLocalY(this.SymbolsContainer.transform.position.y + 0.15f, 0.75f).setEaseOutExpo();
                        break;

                    case TutorialStates.Length:
                        this.LengthContainer.SetActive(true);
                        this.textField.text = "Your password should be at least 8 characters long.";
                        Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords);
                        this.LengthContainer.LeanMoveLocalY(this.LengthContainer.transform.position.y + 0.15f, 0.75f).setEaseOutExpo();
                        break;

                    default:
                        this.textField.gameObject.SetActive(false);
                        this.completeField.gameObject.SetActive(true);
                        this.GameObjects.ForEach(x => x.SetActive(false));
                        Typewriter.AnimateWords(this.completeField, Settings.animationDelayBetweenWords).SetOnComplete(() => this.complete = true);
                        break;
                }

                this.animatingIndex++;
            }
        }

        else if (currentKeyboard[Key.Escape].wasPressedThisFrame) {
            ChangeScene.IncrementScene();
        }
    }

    void AnimateLogos() {
        this.textField.interactable = false;
        this.textField.gameObject.LeanMoveLocalY(this.textField.transform.position.y + 0.3f, 0.75f)
                                 .setEaseOutExpo();
    }

    void DisplayTexts() {
        if (this.currentState >= this.tutorialTexts.Count) {
            this.animatingState = true;
            this.AnimateLogos();
            return;
        }

        this.textField.text = this.tutorialTexts[this.currentState];
        this.typewriterObject = Typewriter.AnimateWords(this.textField, Settings.animationDelayBetweenWords)
                                          .SetOnComplete(() => {
                                              this.sleepObject = Sleep.BeforeFunction(this.DisplayTexts, 4.0f);
                                              this.currentState++;
                                          });
    }
}