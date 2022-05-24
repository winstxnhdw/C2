using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class HUDClipping : MonoBehaviour {
    [SerializeField] float activationPositionZ;
    [SerializeField] TMP_InputField addressField;

    const float maxAlpha = 1.0f;
    const float maxViewDistance = 0.5f;
    const float fadeInDistance = 0.5f;
    CanvasGroup canvasGroup;

    void Awake() {
        if (fadeInDistance > maxViewDistance) throw new Exception("fadeInDistance must be less than or equals to maxViewDistance");

        this.canvasGroup = GetComponent<CanvasGroup>();
        CameraMovementListener.onCameraMove += this.UpdateCanvasAlpha;
        this.UpdateCanvasAlpha();

        this.addressField.onSubmit.AddListener(this.OnSubmit);
    }

    void Update() {
        if (!Keyboard.current[Key.Escape].wasPressedThisFrame) return;
        this.addressField.gameObject.SetActive(!this.addressField.gameObject.activeSelf);
    }

    void UpdateCanvasAlpha() {
        // https://www.desmos.com/calculator/odnqb9ebmt
        float CameraNearClipPlane = Global.MainCamera.transform.position.z + Global.MainCamera.nearClipPlane;
        this.canvasGroup.alpha = maxAlpha * (CameraNearClipPlane - this.activationPositionZ + maxViewDistance) / fadeInDistance;
    }

    void OnSubmit(string address) {
        if (string.IsNullOrWhiteSpace(address)) return;
        LoadAPI.SetAPI(address);
        this.addressField.gameObject.SetActive(false);
    }

    void OnDestroy() {
        CameraMovementListener.onCameraMove -= this.UpdateCanvasAlpha;
    }
}
