using UnityEngine;

public static class Global {
    static Camera camera;

    public static Camera Camera {
        get {
            if (!Global.camera) Global.camera = Camera.main;
            return Global.camera;
        }
    }
}