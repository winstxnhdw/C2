using UnityEngine;

public static class Global {
    static Camera mainCamera;

    public static Camera MainCamera {
        get {
            if (!mainCamera) Global.mainCamera = Camera.main;
            return Global.mainCamera;
        }
    }
}