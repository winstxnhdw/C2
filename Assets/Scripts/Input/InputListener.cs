using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputListener : MonoBehaviour {
    Dictionary<KeyControl, Action> keyActionsDict;

    public static event Action onEnterPress;
    public static event Action onPrintScreenPress;

    void Awake() {
        Keyboard currentKeyboard = Keyboard.current;
        this.keyActionsDict = new Dictionary<KeyControl, Action>() {
            {currentKeyboard[Key.Enter],       () => InputListener.onEnterPress?.Invoke()},
            {currentKeyboard[Key.PrintScreen], () => InputListener.onPrintScreenPress?.Invoke()},
            {currentKeyboard[Key.Escape],            ChangeScene.RestartGame}
        };
    }

    void Update() {
        this.KeyboardListener();
    }

    void KeyboardListener() {
        foreach (KeyValuePair<KeyControl, Action> keyAction in this.keyActionsDict) {
            if (!keyAction.Key.wasPressedThisFrame) continue;
            keyAction.Value();
        }
    }
}