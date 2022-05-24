using System;
using UnityEngine;
using TMPro;

public class HackingField : MonoBehaviour {
    [SerializeField] float animationDurationPerCharacter;
    TMP_InputField hackingField;
    public static event Action onComplete;

    const string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;:\\/<>?~";
    int minPasswordLength;
    float timer;

    void Awake() {
        this.hackingField = GetComponent<TMP_InputField>();
        this.minPasswordLength = 0;
        this.timer = 0.0f;
    }

    void Update() {
        if (this.minPasswordLength == Player.GetChosenPasswordLength()) HackingField.onComplete?.Invoke();
        if (this.timer >= this.animationDurationPerCharacter) {
            this.minPasswordLength++;
            this.timer = 0.0f;
        }

        this.timer += Time.deltaTime;
        this.hackingField.text = StringHelpers.GenerateString(this.minPasswordLength, alphaNumeric);
    }
}