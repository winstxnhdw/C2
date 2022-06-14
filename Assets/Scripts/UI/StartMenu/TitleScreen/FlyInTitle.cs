using UnityEngine;
using Unity.Mathematics;
using TMPro;

public class FlyInTitle : MonoBehaviour {
    [Header("Fly In")]
    [SerializeField] float duration;
    [SerializeField] float fromPositionY;
    [SerializeField][Range(0.0f, 1.0f)] float fromAlpha;
    [SerializeField][Range(0.0f, 1.0f)] float toAlpha;

    [Header("Title")]
    [SerializeField] float maxCharacterSpacing;
    [SerializeField] float maxCameraDistance;
    [SerializeField] float spacingSensitivity;

    float minCharacterSpacing;
    TextMeshProUGUI titleTextMesh;

    void Awake() {
        this.titleTextMesh = GetComponent<TextMeshProUGUI>();
        CameraMovementListener.onCameraMove += this.UpdateTextSpacing;

        this.minCharacterSpacing = this.titleTextMesh.characterSpacing;
        this.UpdateTextSpacing();
    }

    void Start() {
        transform.LeanMoveLocalY(transform.localPosition.y, this.duration)
                 .setFrom(this.fromPositionY)
                 .setEaseOutExpo();

        this.titleTextMesh.LeanAlphaText(this.toAlpha, this.duration)
                          .setFrom(this.fromAlpha)
                          .setEaseOutCubic();
    }

    void UpdateTextSpacing() {
        float deltaCharacterSpacing = this.minCharacterSpacing - this.maxCharacterSpacing;
        this.titleTextMesh.characterSpacing = math.clamp(this.maxCharacterSpacing + (Global.Camera.transform.position.z * this.spacingSensitivity * deltaCharacterSpacing / this.maxCameraDistance), this.minCharacterSpacing, this.maxCharacterSpacing);
    }

    void OnDestroy() {
        CameraMovementListener.onCameraMove -= this.UpdateTextSpacing;
    }
}
