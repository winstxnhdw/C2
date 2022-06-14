using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Typewriter : MonoBehaviour {
    Dictionary<string, Action> animationDict;
    TMP_InputField inputField;
    Action setOnCompleteFunction;
    string text;
    string animationType;
    float delayBetweenText;
    float timer;
    int currentIndex;

    void Initialise(TMP_InputField inputField, float delay) {
        this.inputField = inputField;
        this.text = inputField.text;
        this.delayBetweenText = delay;
        this.timer = delay;
    }

    void InitLetters(TMP_InputField inputField, float delayBetweenLetters) {
        this.Initialise(inputField, delayBetweenLetters);
        this.animationType = "letters";
    }

    void InitWords(TMP_InputField inputField, float delayBetweenWords) {
        this.Initialise(inputField, delayBetweenWords);
        this.animationType = "words";
    }

    void Awake() {
        this.currentIndex = 1;

        this.animationDict = new Dictionary<string, Action> {
            { "letters", this.UpdateLetters },
            { "words", this.UpdateWords }
        };
    }

    void Update() {
        if (!inputField.gameObject.activeInHierarchy) this.EndAnimation();
        this.animationDict[this.animationType]();
    }

    void EndAnimation() {
        if (this.setOnCompleteFunction != null) this.setOnCompleteFunction();

        this.inputField.interactable = false;
        Destroy(gameObject);
    }

    void UpdateWords() {
        string[] words = text.Split(' ');

        if (this.currentIndex > words.Length) {
            this.EndAnimation();
        }

        else if (this.timer >= this.delayBetweenText) {
            this.inputField.text = string.Join(" ", words.Take(this.currentIndex).ToArray());
            this.ResetInputFieldState();
            this.timer = 0.0f;
            this.currentIndex++;
        }

        this.timer += Time.deltaTime;
    }

    void UpdateLetters() {
        if (this.currentIndex > this.text.Length) {
            this.EndAnimation();
        }

        else if (this.timer >= this.delayBetweenText) {
            this.inputField.text = this.text.Substring(0, this.currentIndex);
            this.ResetInputFieldState();
            this.timer = 0.0f;
            this.currentIndex++;
        }

        this.timer += Time.deltaTime;
    }

    void ResetInputFieldState() {
        this.inputField.caretPosition = this.inputField.text.Length;
        this.inputField.ActivateInputField();
    }

    public Typewriter SetOnComplete(Action setOnCompleteFunction) {
        this.setOnCompleteFunction = setOnCompleteFunction;
        return this;
    }

    static Typewriter Animate(TMP_InputField inputField) {
        if (String.IsNullOrEmpty(inputField.text)) throw new Exception("Text is empty");
        inputField.interactable = true;
        return new GameObject("~Typewriter").AddComponent<Typewriter>();
    }

    public static Typewriter AnimateLetters(TMP_InputField inputField, float delayBetweenLetters) {
        Typewriter typewriterObject = Animate(inputField);
        typewriterObject.InitLetters(inputField, delayBetweenLetters);
        return typewriterObject;
    }

    public static Typewriter AnimateWords(TMP_InputField inputField, float delayBetweenWords) {
        Typewriter typewriterObject = Animate(inputField);
        typewriterObject.InitWords(inputField, delayBetweenWords);
        return typewriterObject;
    }
}
