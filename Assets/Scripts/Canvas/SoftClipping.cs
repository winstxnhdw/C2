using System;
using UnityEngine;

// Fades a canvas in when the camera is near.
// Fades a canvas out when the camera is too far.
[RequireComponent(typeof(CanvasGroup))]
public class SoftClipping : MonoBehaviour {
    const float maxAlpha = 1.0f;
    const float maxViewDistance = 4.0f;
    const float fadeInDistance = 2.0f;
    CanvasGroup canvasGroup;

    void Awake() {
        if (fadeInDistance >= maxViewDistance) throw new Exception("fadeInDistance must be less than maxViewDistance");

        this.canvasGroup = GetComponent<CanvasGroup>();
        CameraMovementListener.onCameraMove += this.UpdateCanvasAlpha;
        this.UpdateCanvasAlpha();
    }

    void UpdateCanvasAlpha() {
        // https://www.desmos.com/calculator/odnqb9ebmt
        float CameraNearClipPlane = Global.Camera.transform.position.z + Global.Camera.nearClipPlane;
        this.canvasGroup.alpha = maxAlpha * (CameraNearClipPlane - transform.position.z + maxViewDistance) / fadeInDistance;
    }

    void OnDestroy() {
        CameraMovementListener.onCameraMove -= this.UpdateCanvasAlpha;
    }
}
