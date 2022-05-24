using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public static class StringHelpers {
    static Dictionary<string, Func<string>> interpolationDictionary = new Dictionary<string, Func<string>>() {
        {"{username}", Player.GetUsername}
    };

    public static string GenerateString(int stringLength, in string characters) {
        StringBuilder generatedCharacters = new StringBuilder();

        foreach (int _ in Enumerable.Range(0, stringLength)) {
            generatedCharacters.Append(characters[UnityEngine.Random.Range(0, characters.Length)]);
        }

        return generatedCharacters.ToString();
    }

    public static string GenerateNumbers(int stringLength) {
        return StringHelpers.GenerateString(stringLength, "0123456789");
    }

    public static string InterpolateFieldText(string fieldText) {
        foreach (KeyValuePair<string, Func<string>> interpolationPair in interpolationDictionary) {
            fieldText = fieldText.Replace(interpolationPair.Key, interpolationPair.Value());
        }

        return fieldText;
    }
}