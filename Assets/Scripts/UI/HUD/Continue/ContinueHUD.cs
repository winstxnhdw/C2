using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinueHUD : MonoBehaviour {
    [SerializeField] float duration;
    [SerializeField][Range(0.0f, 1.0f)] float minAlpha;

    TextMeshProUGUI textMesh;
    List<int> tweenIndices;
    bool isEnabled;

    void Awake() {
        this.textMesh = GetComponent<TextMeshProUGUI>();
        this.tweenIndices = new List<int>(2);

        UIManager.setContinue += this.SetContinueHUD;
        UIManager.onError += this.SetErrorColour;
    }

    void BeginFlash() {
        this.tweenIndices.Add(
            this.textMesh.LeanAlphaText(this.minAlpha, this.duration)
                         .setFrom(1.0f)
                         .setLoopPingPong().id
        );
    }

    void SetContinueHUD(bool active) {
        if (active) {
            if (this.isEnabled) return;
            this.isEnabled = true;
            this.EnableContinue();
        }

        else {
            if (!this.isEnabled) return;
            this.isEnabled = false;
            this.DisableContinue();
        }
    }

    void EnableContinue() {
        this.tweenIndices.Add(
            this.textMesh.LeanAlphaText(1.0f, this.duration)
                         .setFrom(0.0f)
                         .setEaseOutExpo()
                         .setOnComplete(this.BeginFlash).id
        );
    }

    void DisableContinue() {
        this.tweenIndices.ForEach(LeanTween.cancel);

        this.textMesh.LeanAlphaText(0.0f, this.duration)
                     .setFrom(this.textMesh.color.a)
                     .setEaseOutExpo();
    }

    void SetErrorColour(Color errorTextColour) {
        this.textMesh.color = ColourChanger.SetColourAlpha(errorTextColour, this.textMesh.color.a);
    }

    void OnDestroy() {
        UIManager.setContinue -= this.SetContinueHUD;
    }
}
