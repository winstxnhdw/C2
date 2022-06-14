using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SubmitScore : MonoBehaviour {
    public void Submit(string username, int score) {
        if (string.IsNullOrEmpty(username)) throw new Exception("Please make sure ScoreCanvas is disabled.");
        StartCoroutine(ISubmit(username, score));
    }

    IEnumerator ISubmit(string username, int score) {
        string serverURL = LoadAPI.ServerURLs.TryGetValue("local", out string localURL) ? localURL : string.Empty;

        if (LoadAPI.ServerURLs.TryGetValue("online", out string URL)) {
            using (UnityWebRequest getRequest = UnityWebRequest.Get(URL)) {
                yield return getRequest.SendWebRequest();

                if (getRequest.result == UnityWebRequest.Result.Success) {
                    serverURL = URL;
                }
            }
        }

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);

        using (UnityWebRequest postRequest = UnityWebRequest.Post(LoadAPI.GetAPI(serverURL), form)) {
            yield return postRequest.SendWebRequest();

            if (postRequest.result != UnityWebRequest.Result.Success) {
                print(postRequest.error);
            }

            else {
                print("Form upload complete!");
            }
        }
    }
}