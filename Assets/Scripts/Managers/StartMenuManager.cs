using UnityEngine;
using Unity.Mathematics;

public class StartMenuManager : MonoBehaviour {
    [SerializeField] float enableDistance;
    [SerializeField] GameObject scrollText;
    [SerializeField] GameObject advisoryCanvas;
    [SerializeField] GameObject advisoryField;
    [SerializeField] GameObject continueCanvas;
    [SerializeField] GameObject continueField;


    void Start() {
        CameraMovementListener.onCameraMove += this.UpdateGameObjects;
        this.UpdateGameObjects();
    }

    void UpdateGameObjects() {
        this.DisableObjectWhenPassed(this.scrollText);

        this.DisableObjectWhenPassed(this.advisoryCanvas);
        this.EnableObjectWhenNear(this.advisoryField);

        this.DisableObjectWhenPassed(this.continueCanvas);
        this.EnableObjectWhenNear(this.continueField);
    }

    void DisableObjectWhenPassed(GameObject gameObject) {
        gameObject.SetActive(Global.MainCamera.transform.position.z <= gameObject.transform.position.z);
    }

    void EnableObjectWhenNear(GameObject gameObject) {
        gameObject.SetActive(math.abs(Global.MainCamera.transform.position.z - gameObject.transform.position.z) < enableDistance);
    }

    void OnDestroy() {
        CameraMovementListener.onCameraMove -= this.UpdateGameObjects;
    }
}