using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HUDClipping : MonoBehaviour {
    [SerializeField] float activationPositionZ;

    const float maxAlpha = 1.0f;
    const float maxViewDistance = 0.5f;
    const float fadeInDistance = 0.5f;
    CanvasGroup canvasGroup;

    void Awake() {
        if (fadeInDistance > maxViewDistance) throw new Exception("fadeInDistance must be less than or equals to maxViewDistance");

        this.canvasGroup = GetComponent<CanvasGroup>();
        CameraMovementListener.onCameraMove += this.UpdateCanvasAlpha;
        this.UpdateCanvasAlpha();
    }

    void UpdateCanvasAlpha() {
        // https://www.desmos.com/calculator/odnqb9ebmt
        float CameraNearClipPlane = Global.MainCamera.transform.position.z + Global.MainCamera.nearClipPlane;
        this.canvasGroup.alpha = maxAlpha * (CameraNearClipPlane - this.activationPositionZ + maxViewDistance) / fadeInDistance;
    }

    void OnDestroy() {
        CameraMovementListener.onCameraMove -= this.UpdateCanvasAlpha;
    }
}
