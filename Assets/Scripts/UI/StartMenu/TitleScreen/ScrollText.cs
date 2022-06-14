using UnityEngine;

public class ScrollText : MonoBehaviour {
    [Header("Flashing Effect")]
    [SerializeField] float duration;
    [SerializeField][Range(0.0f, 1.0f)] float minAlpha;

    [Header("Rotation Effect")]
    [SerializeField] float rotationSensitivity;

    SpriteRenderer flashingSprite;
    int tweenIndex;

    void Awake() {
        this.flashingSprite = GetComponent<SpriteRenderer>();
        this.flashingSprite.color = Settings.DefaultTextColour;
        CameraMovementListener.onCameraMove += this.UpdateSpriteAngle;
    }

    void OnEnable() {
        this.tweenIndex = this.flashingSprite.LeanAlpha(this.minAlpha, this.duration)
                                             .setFrom(1.0f)
                                             .setLoopPingPong().id;
    }

    void UpdateSpriteAngle() {
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -Global.Camera.transform.position.z * this.rotationSensitivity);
    }

    void OnDestroy() {
        LeanTween.cancel(this.tweenIndex);
        CameraMovementListener.onCameraMove -= this.UpdateSpriteAngle;
    }
}