using UnityEngine;
using Unity.Mathematics;

public static class ScreenInfo {
    public static float3 GetScreenCentre3D(float z = 0.0f) {
        float3 screenCentre = new float3(Screen.width, Screen.height, z) * 0.5f;
        return Global.Camera.ScreenToWorldPoint(screenCentre);
    }

    public static float2 GetScreenCentre2D() {
        return GetScreenCentre3D().xy;
    }
}