using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ChangeScene {
    static int GetNextSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }

    public static void IncrementScene() {
        SceneManager.LoadScene(GetNextSceneIndex());
    }

    public static void RestartGame() {
        Player.Reset();
        SceneManager.LoadScene(1);
    }
}