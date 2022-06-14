using System;
using UnityEngine;

public class CameraMovementListener : MonoBehaviour {
    public static event Action onCameraMove;
    float previousCameraPositionZ;

    void Update() {
        float mainCameraPosZ = Global.Camera.transform.position.z;
        if (previousCameraPositionZ == mainCameraPosZ) return;

        previousCameraPositionZ = mainCameraPosZ;
        CameraMovementListener.onCameraMove?.Invoke();
    }
}
