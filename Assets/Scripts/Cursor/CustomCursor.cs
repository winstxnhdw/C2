using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Unity.Mathematics;

[RequireComponent(typeof(CursorMovement))]
public class CustomCursor : MonoBehaviour {
    [SerializeField] float zOffsetFromNearClipPlane;
    CursorMovement cursorMovement;
    Vector2Control currentMousePosition;
    float zOffsetFromCamera;

    void Awake() {
        UIManager.onError += this.SetErrorCursor;
        CameraMovementListener.onCameraMove += this.UpdateVirtualCursorPosition;
        CursorMovement.onVirtualCursorMove += this.UpdateVirtualCursorPosition;

        this.cursorMovement = GetComponent<CursorMovement>();
        this.currentMousePosition = Mouse.current.position;
        this.zOffsetFromCamera = Global.MainCamera.nearClipPlane + this.zOffsetFromNearClipPlane;
        this.UpdateVirtualCursorPosition();
    }

    void Update() {
        Vector2 mousePosition = currentMousePosition.ReadValue();
        Vector3 realCursorPosition = new Vector3(mousePosition.x, mousePosition.y, this.zOffsetFromCamera);
        this.cursorMovement.FollowRealCursor(realCursorPosition);
        transform.localEulerAngles = Global.MainCamera.transform.localEulerAngles;
    }

    void UpdateVirtualCursorPosition() {
        Vector2 mousePosition = currentMousePosition.ReadValue();
        Vector3 realCursorPosition = new Vector3(mousePosition.x, mousePosition.y, this.zOffsetFromCamera);
        Vector3 virtualCursorPosition = Global.MainCamera.ScreenToWorldPoint(realCursorPosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, virtualCursorPosition.z);
    }

    void UpdateVirtualCursorPosition(in float2 _) {
        this.UpdateVirtualCursorPosition();
    }

    void SetErrorCursor(Color errorTextColour) {
        GetComponent<SpriteRenderer>().color = errorTextColour;
    }

    void SetDefaultCursor(Color defaultTextColour) {
        GetComponent<SpriteRenderer>().color = defaultTextColour;
    }

    void OnDestroy() {
        UIManager.onError -= this.SetErrorCursor;
        CameraMovementListener.onCameraMove -= this.UpdateVirtualCursorPosition;
        CursorMovement.onVirtualCursorMove -= this.UpdateVirtualCursorPosition;
    }
}