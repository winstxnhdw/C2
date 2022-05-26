using UnityEngine;

public class RuntimeSettings : MonoBehaviour {
    void Awake() {
        // Application
        Application.targetFrameRate = -1;

        // Cursor
        Cursor.visible = Settings.CursorVisible;
        Cursor.lockState = Settings.CursorConfined ? CursorLockMode.Confined : CursorLockMode.None;

        // Camera
        Global.MainCamera.backgroundColor = Settings.CameraBackground;
    }
}