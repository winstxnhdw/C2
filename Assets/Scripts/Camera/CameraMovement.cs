using UnityEngine;
using Unity.Mathematics;

public class CameraMovement : MonoBehaviour {
    [SerializeField] bool isEnabled;
    [SerializeField] float zDeltaScroll;
    [SerializeField] float tweenDuration;

    // +ve is forward, -ve is down
    public void MoveZ(float value) {
        if (value == 0.0f || !this.isEnabled) return;

        float newPositionZ = math.clamp(transform.position.z + (math.sign(value) * this.zDeltaScroll), Settings.zMinScrollStartMenu, Settings.zMaxScrollStartMenu);
        LeanTween.cancel(gameObject);
        transform.LeanMoveLocalZ(newPositionZ, this.tweenDuration)
                 .setEaseOutExpo();
    }
}
