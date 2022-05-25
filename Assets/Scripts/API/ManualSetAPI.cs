using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ManualSetAPI : MonoBehaviour {
    [SerializeField] TMP_InputField addressField;

    void Awake() {
        this.addressField.onSubmit.AddListener(this.OnSubmit);
    }

    void Update() {
        if (!Keyboard.current[Key.Escape].wasPressedThisFrame) return;
        this.addressField.gameObject.SetActive(!this.addressField.gameObject.activeSelf);
    }

    void OnSubmit(string address) {
        if (string.IsNullOrWhiteSpace(address)) return;
        LoadAPI.SetAPI(address);
        this.addressField.gameObject.SetActive(false);
    }

    void OnDestroy() {
        this.addressField.onSubmit.RemoveAllListeners();
    }
}