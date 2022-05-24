using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {
    [Header("Images")]
    [SerializeField] Image fillImage;
    [SerializeField] Image fillImageDelayed;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer outerCircleSprite;
    [SerializeField] SpriteRenderer outerCircleShadowSprite;
    [SerializeField] SpriteRenderer startCapSprite;
    [SerializeField] GameObject endCapObject;

    Vector3 totalRotation;

    void Awake() {
        this.totalRotation = new Vector3(0.0f, 0.0f, -360.0f);
    }

    void OnEnable() {
        if (Player.HasBeenBruteforced()) this.OnError(Settings.errorTextColour);
    }

    public void AnimateScoreBar(int totalScore, int maxScore, float animationDuration) {
        float fillAmount = (float)totalScore / (float)maxScore;

        LeanTween.value(0.0f, fillAmount, animationDuration)
                 .setEaseOutExpo()
                 .setOnUpdate((float value) => {
                     this.fillImage.fillAmount = value;
                     this.endCapObject.transform.localEulerAngles = this.totalRotation * value;
                 });

        LeanTween.value(0.0f, fillAmount, animationDuration)
                 .setEaseOutQuint()
                 .setOnUpdate((float value) => this.fillImageDelayed.fillAmount = value);
    }

    void OnError(in Color errorTextColour) {
        SpriteRenderer endCapSprite = this.endCapObject.GetComponentInChildren<SpriteRenderer>();

        this.fillImage.color = ColourChanger.SetColourAlpha(errorTextColour, this.fillImage.color.a);
        this.fillImageDelayed.color = ColourChanger.SetColourAlpha(errorTextColour, this.fillImageDelayed.color.a);
        this.outerCircleSprite.color = ColourChanger.SetColourAlpha(errorTextColour, this.outerCircleSprite.color.a);
        this.outerCircleShadowSprite.color = ColourChanger.SetColourAlpha(errorTextColour, this.outerCircleShadowSprite.color.a);
        this.startCapSprite.color = ColourChanger.SetColourAlpha(errorTextColour, this.startCapSprite.color.a);
        endCapSprite.color = ColourChanger.SetColourAlpha(errorTextColour, endCapSprite.color.a);
    }
}