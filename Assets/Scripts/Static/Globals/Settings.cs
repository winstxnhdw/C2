using System;
using System.Collections.Generic;
using UnityEngine;

public class Settings {
    static Func<string, string> DictionaryFilePath => fileName => $"Dictionaries/{fileName}";
    public static Color CameraBackground => ColourChanger.HexToColor("0C0C0C");
    public static Color DefaultTextColour => ColourChanger.HexToColor("03A062");
    public static Color ErrorTextColour => ColourChanger.HexToColor("900000");
    public static string ErrorTextHex => "#900000";
    public static float AnimationDelayBetweenWords => 0.1f;
    public static float AnimationDelayBetweenLetters => 0.02f;
    public static float MinScrollZ => 0.0f;
    public static float MaxScrollZ => 7.0f;
    public static int MinPasswordLength => 8;
    public static int MaxRecallAttempts => 3;

#if UNITY_EDITOR
    public static string DictionaryFileName => DictionaryFilePath("test");
    public static bool CursorVisible => true;
    public static bool CursorConfined => false;
    public static float LogAnimationDelay => 0.0f;
    public static float HackerDetectedDelay => 1.0f;
    public static Dictionary<int, string> PasswordOrdinalIndicators => new Dictionary<int, string>() {
        {0, "1<sup>st</sup>"},
    };

#else
    public static string DictionaryFileName => DictionaryFilePath("rockyou8");
    public static bool CursorVisible => false;
    public static bool CursorConfined => true;
    public static float LogAnimationDelay => 0.5f;
    public static float HackerDetectedDelay => 3.0f;
    public static Dictionary<int, string> PasswordOrdinalIndicators => new Dictionary<int, string>() {
        {0, "1<sup>st</sup>"},
        {1, "2<sup>nd</sup>"},
        {2, "3<sup>rd</sup>"}
    };

#endif
}
