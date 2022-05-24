using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SubmitScore : MonoBehaviour {
    public void Submit(in string username, int score) {
        if (string.IsNullOrEmpty(username)) throw new Exception("Please make sure ScoreCanvas is disabled.");
        StartCoroutine(ISubmit(username, score));
    }

    IEnumerator ISubmit(string username, int score) {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(LoadAPI.GetAPI, form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                print(www.error);
            }

            else {
                print("Form upload complete!");
            }
        }
    }
}