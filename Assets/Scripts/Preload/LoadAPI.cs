using UnityEngine;

public class LoadAPI : MonoBehaviour {
    static string API;
    static string URI;

    void Awake() {
        LoadAPI.URI = Resources.Load<TextAsset>("API/uri").text;
        LoadAPI.API = $"{Resources.Load<TextAsset>("API/url").text}/{LoadAPI.URI}";
    }

    public static string GetAPI {
        get {
            return LoadAPI.API;
        }
    }

    public static void SetAPI(string url) {
        LoadAPI.API = $"{url}/{LoadAPI.URI}";
    }
}