using System;
using UnityEngine;
using TMPro;

public class RecallInstructions : MonoBehaviour {
    [TextArea][SerializeField] string instructionsText;

    public static event Action onComplete;
    public static event Action onIncorrectRecall;

    TextMeshProUGUI instructionsTextMesh;
    int currentAttempts;

    void Awake() {
        RecallReply.onComplete += this.UpdateInstructions;
        this.instructionsTextMesh = GetComponent<TextMeshProUGUI>();
        this.instructionsTextMesh.text = this.instructionsText.Replace("{recallAttempts}", Settings.MaxRecallAttempts.ToString());
        this.currentAttempts = 1;
    }

    void UpdateInstructions() {
        if (this.currentAttempts >= Settings.MaxRecallAttempts || Player.IsRecalledPasswordCorrect()) {
            RecallInstructions.onComplete?.Invoke();
            return;
        }

        RecallInstructions.onIncorrectRecall?.Invoke();
        string remainingAttempts = (Settings.MaxRecallAttempts - this.currentAttempts).ToString();
        this.instructionsTextMesh.text = this.instructionsText.Replace("{recallAttempts}", remainingAttempts);
        this.currentAttempts++;
    }

    void OnDestroy() {
        RecallReply.onComplete -= this.UpdateInstructions;
    }
}