using System;
using System.Linq;
using System.Collections.Generic;

public static class Player {
    static string username;
    static bool hasBeenBruteforced;
    static List<string> passwords = new List<string>(Settings.passwordOrdinalIndicators.Count());

    static int chosenPasswordIndex;
    static string recalledPasswordAttempt;

    public static void CreateUsername(string username) {
        Player.username = username;
    }

    public static string GetUsername() {
        return Player.username;
    }

    public static void AddPassword(string password) {
        Player.passwords.Add(password);
    }

    public static IEnumerable<string> GetPasswords() {
        return Player.passwords;
    }

    public static string GetPasswordAtIndex(int index) {
        return Player.passwords[index];
    }

    public static int GetPasswordCount() {
        return Player.passwords.Count;
    }

    public static string GetPasswordOrdinalIndicator(int index) {
        return Settings.passwordOrdinalIndicators[index];
    }

    public static string GetCurrentPasswordOrdinalIndicator() {
        return Player.GetPasswordOrdinalIndicator(Player.GetPasswordCount());
    }

    public static void RemoveLastPassword() {
        Player.passwords.RemoveAt(Player.GetPasswordCount() - 1);
    }

    public static void SetChosenPasswordIndex(int index) {
        Player.chosenPasswordIndex = index;
    }

    public static int GetChosenPasswordLength() {
        return Player.GetPasswordAtIndex(Player.chosenPasswordIndex).Length;
    }

    public static void SetRecalledPasswordAttempt(string attempt) {
        Player.recalledPasswordAttempt = attempt;
    }

    public static bool IsRecalledPasswordCorrect() {
        if (string.IsNullOrEmpty(Player.recalledPasswordAttempt)) throw new Exception("No password attempt has been set.");
        return Player.recalledPasswordAttempt == Player.GetPasswordAtIndex(Player.chosenPasswordIndex);
    }

    public static bool IsPasswordBruteforceable() {
        if (PasswordList.PasswordSet == null) throw new Exception("Password list is empty.");
        Player.hasBeenBruteforced = PasswordList.PasswordSet.Contains(Player.GetPasswordAtIndex(Player.chosenPasswordIndex));
        return Player.hasBeenBruteforced;
    }

    public static bool HasBeenBruteforced() {
        return Player.hasBeenBruteforced;
    }

    public static void Reset() {
        Player.username = string.Empty;
        Player.passwords.Clear();
    }
}