using System;
using System.Linq;
using System.Collections.Generic;

public static class Player {
    static List<string> passwords = new List<string>(Settings.passwordOrdinalIndicators.Count());
    static string recalledPasswordAttempt;
    static int chosenPasswordIndex;

    public static string Username { get; set; }

    public static string CurrentPasswordOrdinalIndicator => Player.GetPasswordOrdinalIndicator(Player.PasswordCount);

    public static int PasswordCount => Player.passwords.Count;

    public static int ChosenPasswordLength => Player.GetPasswordAtIndex(Player.chosenPasswordIndex).Length;

    public static IEnumerable<string> Passwords => Player.passwords;

    public static void AddPassword(string password) => Player.passwords.Add(password);

    public static string GetPasswordAtIndex(int index) => Player.passwords[index];

    public static string GetPasswordOrdinalIndicator(int index) => Settings.passwordOrdinalIndicators[index];

    public static void RemoveLastPassword() => Player.passwords.RemoveAt(Player.PasswordCount - 1);

    public static void SetChosenPasswordIndex(int index) => Player.chosenPasswordIndex = index;

    public static void SetRecalledPasswordAttempt(string attempt) => Player.recalledPasswordAttempt = attempt;

    public static bool IsRecalledPasswordCorrect() {
        if (string.IsNullOrEmpty(Player.recalledPasswordAttempt)) throw new Exception("No password attempt has been set.");
        return Player.recalledPasswordAttempt == Player.GetPasswordAtIndex(Player.chosenPasswordIndex);
    }

    public static bool HasBeenBruteforced() {
        if (PasswordList.PasswordSet == null) throw new Exception("Password list is empty.");
        return PasswordList.PasswordSet.Contains(Player.GetPasswordAtIndex(Player.chosenPasswordIndex));
    }

    public static void Reset() {
        Player.Username = string.Empty;
        Player.passwords.Clear();
    }
}