using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class PasswordList : MonoBehaviour {
    static HashSet<string> passwordSet;

    void Awake() {
        string passwordStrings = Resources.Load<TextAsset>(Settings.DictionaryFileName).text;

        Thread initialisePasswordSetThread = new Thread(() => this.InitialisePasswordSet(passwordStrings));
        initialisePasswordSetThread.Start();
    }

    void InitialisePasswordSet(string passwordStrings) {
        PasswordList.passwordSet = new HashSet<string>(passwordStrings.Split('\n'));
    }

    public static HashSet<string> PasswordSet {
        get {
            return PasswordList.passwordSet;
        }
    }
}
