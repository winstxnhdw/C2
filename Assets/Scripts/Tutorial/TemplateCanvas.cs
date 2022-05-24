using System;
using UnityEngine;
using TMPro;

public class TemplateCanvas : MonoBehaviour {
    public static event Action onComplete;
    protected TMP_InputField inputField;
    protected Typewriter typewriterObject;
    protected string inputFieldText;
    protected bool readyToInvoke;
    bool isInvoking;

    void Start() {
        this.inputField = GetComponentInChildren<TMP_InputField>();
        this.inputFieldText = this.inputField.text;
        this.isInvoking = false;
        this.readyToInvoke = false;
        this.inputField.text = "";
    }

    void Update() {
        if (!this.readyToInvoke) return;
        if (this.isInvoking) return;

        Sleep.BeforeFunction(this.OnComplete, 1.0f);
        this.isInvoking = true;
    }

    protected void InvokeAction() {
        TemplateCanvas.onComplete?.Invoke();
    }

    void OnComplete() {
        this.InvokeAction();
        Destroy(this.gameObject);
    }
}