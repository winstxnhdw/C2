using System;
using System.Collections.Generic;
using UnityEngine;

public class Settings {
    static readonly Func<string, string> dictionaryFilePath = fileName => $"Dictionaries/{fileName}";
    public static readonly Color cameraBackground = ColourChanger.HexToColor("0C0C0C");
    public static readonly Color defaultTextColour = ColourChanger.HexToColor("03A062");
    public static readonly Color errorTextColour = ColourChanger.HexToColor("900000");
    public static readonly string errorTextHex = "#900000";
    public static readonly float animationDelayBetweenWords = 0.1f;
    public static readonly float animationDelayBetweenLetters = 0.02f;
    public static readonly int minPasswordLength = 8;
    public static readonly float zMinScrollStartMenu = 0.0f;
    public static readonly float zMaxScrollStartMenu = 7.0f;

#if UNITY_EDITOR
    public static readonly string dictionaryFileName = dictionaryFilePath("test");
    public static readonly bool cursorVisible = true;
    public static readonly bool cursorConfined = false;
    public static readonly float logAnimationDelay = 0.0f;
    public static readonly float hackerDetectedDelay = 1.0f;
    public static readonly Dictionary<int, string> passwordOrdinalIndicators = new Dictionary<int, string>() {
        {0, "1<sup>st</sup>"},
    };

#else
    public static readonly string dictionaryFileName = dictionaryFilePath("rockyou8");
    public static readonly bool cursorVisible = false;
    public static readonly bool cursorConfined = true;
    public static readonly float logAnimationDelay = 0.5f;
    public static readonly float hackerDetectedDelay = 3.0f;
    public static readonly Dictionary<int, string> passwordOrdinalIndicators = new Dictionary<int, string>() {
        {0, "1<sup>st</sup>"},
        {1, "2<sup>nd</sup>"},
        {2, "3<sup>rd</sup>"}
    };

#endif
}
