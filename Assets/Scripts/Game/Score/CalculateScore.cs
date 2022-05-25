using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;

[RequireComponent(typeof(SubmitScore), typeof(ScoreBar))]
public class CalculateScore : MonoBehaviour {
    [Header("Recommendation Field")]
    [SerializeField] float recommendationFlyInOffset;
    [SerializeField][Range(0.0f, 1.0f)] float recommendationToAlpha;
    [SerializeField][Range(0.0f, 1.0f)] float recommendationFromAlpha;
    [SerializeField] float recommendationAnimationDuration;
    [SerializeField] TextMeshProUGUI recommendationTextMesh;

    [Header("Score Bar")]
    [SerializeField] float scoreAnimationDuration;
    [SerializeField] TextMeshProUGUI scoreTextMesh;
    [SerializeField] TextMeshProUGUI labelTextMesh;

    public static event Action<bool> setContinueHUD;

    Dictionary<int, string> recommendationDict;
    SubmitScore submitScore;
    ScoreBar scoreBar;
    bool completed;

    void Awake() {
        InputListener.onEnterPress += this.RestartGameOnComplete;

        this.submitScore = GetComponent<SubmitScore>();
        this.scoreBar = GetComponent<ScoreBar>();
        this.completed = false;

        this.recommendationDict = new Dictionary<int, string> {
            {1, "Your passwords have likely been compromised.\nPlease ensure that your passwords are much longer and complex."},
            {2, "Your passwords may be compromised.\nDo consider improving the length and complexity of the password."},
            {3, "Your passwords are sufficiently complex.\nHowever, do remember to change your passwords regularly."}
        };
    }

    void OnEnable() {
        if (Player.HasBeenBruteforced()) this.OnError(Settings.ErrorTextColour);

        int totalScore = this.CalculatePasswordScores();
        this.AnimateScore(totalScore);
        this.scoreBar.AnimateScoreBar(totalScore, 300, this.scoreAnimationDuration);
        this.submitScore.Submit(Player.Username, totalScore);
    }

    void RestartGameOnComplete() {
        if (!this.completed) return;
        ChangeScene.RestartGame();
    }

    int CalculatePasswordScores() {
        int totalScore = 0;

        foreach (int index in Enumerable.Range(0, Player.PasswordCount)) {
            int score = this.CalculatePasswordScore(Player.GetPasswordAtIndex(index));
            totalScore += score;
        }

        return math.clamp(Player.HasBeenBruteforced() ? totalScore - 100 : totalScore, 0, 300);
    }

    void AnimateScore(int totalScore) {
        if (totalScore == 0) {
            this.OnTweenComplete(totalScore);
        }

        else {
            LeanTween.value(0.0f, totalScore, this.scoreAnimationDuration)
                     .setEaseOutExpo()
                     .setOnComplete(() => this.OnTweenComplete(totalScore))
                     .setOnUpdate((float value) => this.scoreTextMesh.text = Mathf.RoundToInt(value).ToString());
        }
    }

    void OnTweenComplete(int totalScore) {
        int normalisedScore = (int)math.ceil((totalScore + 0.01f) / 100.0f);
        this.recommendationTextMesh.text = this.recommendationDict[normalisedScore];
        float textPositionY = this.recommendationTextMesh.transform.localPosition.y;

        this.recommendationTextMesh.transform
                                   .LeanMoveLocalY(textPositionY, this.recommendationAnimationDuration)
                                   .setFrom(textPositionY - this.recommendationFlyInOffset)
                                   .setEaseOutExpo();

        this.recommendationTextMesh.LeanAlphaText(this.recommendationToAlpha, this.recommendationAnimationDuration)
                                   .setFrom(this.recommendationFromAlpha)
                                   .setEaseOutExpo();

        this.completed = true;
        CalculateScore.setContinueHUD?.Invoke(this.completed);
    }

    int CalculatePasswordScore(in string password) {
        /* 
        Ensures that the password has a combination of different type of characters since common passwords tend not to have both, or have 1 or 2 of one and the rest of the password is just made of the other
        shorter passwords are easier to crack, limited to 20 so that the user can't easily score points
        to ensure that the password consists of atleast 1 digit/symbol/upper/lower character and to encourage the user to put more of them to have a safer password
        give more points to the user for using more digits and symbols as people usually only put letters in their passwords,
        limited it so that the user doesn't retry it and only put that character in the password
        */

        int points = 0;

        if (password.Count(char.IsUpper) >= 3 && password.Count(char.IsLower) >= 3) points += 3;
        if (password.Count(char.IsLetter) >= 3 && password.Count(char.IsDigit) >= 3) points += 3;

        points += math.clamp(password.Length, 0, 20);
        points += 3 * math.clamp(password.Length - password.Count(char.IsLetterOrDigit), 0, 3);
        points += 3 * math.clamp(password.Count(char.IsDigit), 0, 3);
        points += 1 * math.clamp(password.Count(char.IsUpper), 0, 3);
        points += 1 * math.clamp(password.Count(char.IsLower), 0, 3);

        return math.clamp(2 * points, 0, 100);
    }

    void OnError(Color errorTextColour) {
        this.recommendationTextMesh.color = ColourChanger.SetColourAlpha(errorTextColour, this.recommendationTextMesh.color.a);
        this.scoreTextMesh.color = errorTextColour;
        this.labelTextMesh.color = errorTextColour;
    }

    void OnDestroy() {
        InputListener.onEnterPress -= this.RestartGameOnComplete;
    }
}