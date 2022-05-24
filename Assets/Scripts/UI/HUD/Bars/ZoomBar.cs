using UnityEngine;
using UnityEngine.UI;

public class ZoomBar : MonoBehaviour {
    Image barMask;

    void Awake() {
        this.barMask = transform.GetChild(0).GetComponent<Image>();
        CameraMovementListener.onCameraMove += FillCurrentBars;
    }

    void FillCurrentBars() {
        this.barMask.fillAmount = Global.MainCamera.transform.position.z / Settings.zMaxScrollStartMenu;
    }

    void OnDestroy() {
        CameraMovementListener.onCameraMove -= FillCurrentBars;
    }
}