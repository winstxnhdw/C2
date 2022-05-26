using System.Collections.Generic;
using UnityEngine;

public class LoadAPI : MonoBehaviour {
    static string URI => Resources.Load<TextAsset>("API/uri").text;

    public static Dictionary<string, string> ServerURLs => new Dictionary<string, string> {
        { "online", Resources.Load<TextAsset>("API/url").text },
        { "local",  Resources.Load<TextAsset>("API/url_local").text }
    };

    public static string GetAPI(string URL) => $"{URL}/{URI}";
}