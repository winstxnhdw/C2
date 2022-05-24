using UnityEngine;
using TMPro;

public class StartMenuField : MonoBehaviour {
    [TextArea][SerializeField] string inputFieldText;
    TMP_InputField inputField;

    void Awake() {
        inputField = GetComponent<TMP_InputField>();
    }

    void OnEnable() {
        inputField.text = this.inputFieldText;
        Typewriter.AnimateLetters(inputField, Settings.animationDelayBetweenLetters);
    }
}