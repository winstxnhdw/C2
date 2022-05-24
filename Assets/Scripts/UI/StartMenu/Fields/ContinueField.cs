using UnityEngine.InputSystem;

public class ContinueField : StartMenuField {
    void Update() {
        if (!Keyboard.current.anyKey.wasPressedThisFrame) return;

        ChangeScene.IncrementScene();
    }
}