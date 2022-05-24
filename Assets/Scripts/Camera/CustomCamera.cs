using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Unity.Mathematics;

[RequireComponent(typeof(CameraMovement), typeof(CameraPan))]
public class CustomCamera : MonoBehaviour {
    CameraMovement cameraMovement;
    CameraPan cameraPan;
    Vector2Control currentMouseScroll;

    void Awake() {
        CursorMovement.onVirtualCursorMove += this.UpdateCameraRotation;
        Global.MainCamera.backgroundColor = Settings.cameraBackground;

        this.currentMouseScroll = Mouse.current.scroll;
        this.cameraMovement = GetComponent<CameraMovement>();
        this.cameraPan = GetComponent<CameraPan>();
    }

    void Update() {
        this.cameraMovement.MoveZ(this.currentMouseScroll.ReadValue().y);
    }

    void UpdateCameraRotation(in float2 objectPosition) {
        this.cameraPan.PanFollow(objectPosition);
    }

    void OnDestroy() {
        CursorMovement.onVirtualCursorMove -= this.UpdateCameraRotation;
    }
}
