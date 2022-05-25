using UnityEngine;
using TMPro;

public class PromptField : MonoBehaviour {
    [TextArea][SerializeField] protected string inputFieldText;
    protected TMP_InputField promptField;

    void Awake() {
        this.promptField = GetComponent<TMP_InputField>();
        this.promptField.text = this.inputFieldText;
    }

    protected virtual void OnEnable() {
        this.promptField.interactable = true;
        Typewriter.AnimateWords(this.promptField, Settings.AnimationDelayBetweenWords)
                  .SetOnComplete(() => this.InvokeOnComplete());
    }

    protected virtual void InvokeOnComplete() { }

}