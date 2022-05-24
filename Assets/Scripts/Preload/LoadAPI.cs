using UnityEngine;

public class LoadAPI : MonoBehaviour {
    static string API;

    void Awake() {
        LoadAPI.API = Resources.Load<TextAsset>("API/url").text;
    }

    public static string GetAPI {
        get {
            return LoadAPI.API;
        }
    }
}