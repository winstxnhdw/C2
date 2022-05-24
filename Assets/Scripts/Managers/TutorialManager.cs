using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour {
    public static event Action beginWelcome;
    public static event Action beginExplanationOne;
    public static event Action beginExplanationTwo;
    public static event Action animateExplanationTwo;
    public static event Action onCompleteTutorial;

    int tutorialIndex;

    void Awake() {
        TemplateCanvas.onComplete += this.UpdateTutorial;
        this.tutorialIndex = 0;
    }

    void Start() {
        this.UpdateTutorial();
    }

    void Update() {
        if (!Keyboard.current[Key.Enter].wasPressedThisFrame) return;
        ChangeScene.IncrementScene();
    }

    void UpdateTutorial() {
        this.tutorialIndex++;

        switch (this.tutorialIndex) {
            case 1:
                TutorialManager.beginWelcome?.Invoke();
                break;

            case 2:
                TutorialManager.beginExplanationOne?.Invoke();
                break;

            case 3:
                TutorialManager.beginExplanationTwo?.Invoke();
                break;

            case 4:
                TutorialManager.animateExplanationTwo?.Invoke();
                break;

            default:
                TutorialManager.onCompleteTutorial?.Invoke();
                break;
        }
    }

    void OnDestroy() {
        TemplateCanvas.onComplete -= this.UpdateTutorial;
    }
}