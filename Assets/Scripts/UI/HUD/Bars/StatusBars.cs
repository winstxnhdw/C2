using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBars : MonoBehaviour {
    [Header("Parameters")]
    [SerializeField] float barAnimationDuration;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI barTextOne;
    [SerializeField] TextMeshProUGUI barTextTwo;
    [SerializeField] TextMeshProUGUI barTextThree;
    [SerializeField] TextMeshProUGUI barTextFour;
    [SerializeField] TextMeshProUGUI barTextFive;
    [SerializeField] TextMeshProUGUI barTextSix;

    [Header("Masks")]
    [SerializeField] Image signalsLogo;
    [SerializeField] Image barMaskOne;
    [SerializeField] Image barMaskTwo;
    [SerializeField] Image barMaskThree;
    [SerializeField] Image barMaskFour;
    [SerializeField] Image barMaskFive;
    [SerializeField] Image barMaskSix;

    const int barMaskMaxSlice = 2;
    int barMasksIndex;
    List<Image> barMasks;
    List<TextMeshProUGUI> statusBarTexts;

    void Awake() {
        UIManager.onError += SetColoursToError;
        PasswordVerification.onSuccessfulValidation += FillCurrentBars;

        this.statusBarTexts = new List<TextMeshProUGUI> {
            this.nameText,
            this.barTextOne,
            this.barTextTwo,
            this.barTextThree,
            this.barTextFour,
            this.barTextFive,
            this.barTextSix
        };

        this.barMasks = new List<Image> {
            this.barMaskOne,
            this.barMaskTwo,
            this.barMaskThree,
            this.barMaskFour,
            this.barMaskFive,
            this.barMaskSix
        };

        this.barMasksIndex = 0;
    }

    void FillCurrentBars() {
        this.barMasks.Skip(this.barMasksIndex)
                     .Take(barMaskMaxSlice)
                     .ToList()
                     .ForEach(mask => {
                         LeanTween.value(mask.fillAmount, 1.0f, this.barAnimationDuration)
                                  .setEaseInOutExpo()
                                  .setOnUpdate((float value) => mask.fillAmount = value);
                     });

        this.barMasksIndex += barMaskMaxSlice;
    }

    void SetColoursToError(Color errorTextColour) {
        this.statusBarTexts.ForEach(text => text.color = errorTextColour);
        this.barMasks.ForEach(mask => mask.transform.GetChild(0).GetComponent<Image>().color = errorTextColour);
        this.signalsLogo.color = ColourChanger.SetColourAlpha(errorTextColour, this.signalsLogo.color.a);
    }

    void OnDestroy() {
        UIManager.onError -= SetColoursToError;
        PasswordVerification.onSuccessfulValidation -= FillCurrentBars;
    }
}