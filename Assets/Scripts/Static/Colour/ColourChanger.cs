using System.Globalization;
using UnityEngine;

public static class ColourChanger {
    public static Color SetColourAlpha(Color currentColour, float alpha) {
        return new Color(currentColour.r, currentColour.g, currentColour.b, alpha);
    }

    public static Color HexToColor(string hex) {
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    public static string SetErrorTextColour(string text) {
        return $"<color={Settings.ErrorTextHex}>{text}</color>";
    }
}