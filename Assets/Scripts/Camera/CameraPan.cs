using UnityEngine;
using Unity.Mathematics;

public class CameraPan : MonoBehaviour {
    [SerializeField] float panSensitivity;
    float2 screenToWorldCentre2D;

    void Awake() {
        this.screenToWorldCentre2D = ScreenInfo.GetScreenCentre2D();
    }

    // Pan camera to follow object on screen
    public void PanFollow(float2 objectPosition) {
        float2 translatedCursorPosition = objectPosition - this.screenToWorldCentre2D;
        transform.localEulerAngles = new Vector3(-translatedCursorPosition.y, translatedCursorPosition.x, 0.0f) * this.panSensitivity;
    }
}
