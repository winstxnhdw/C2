using System;
using UnityEngine;

public class Sleep : MonoBehaviour {
    Action function;
    Action setOnCompleteFunction;
    float timer;
    float delay;

    void Initialise(Action function, float delay) {
        this.function = function;
        this.delay = delay;
    }

    void Awake() {
        this.timer = 0.0f;
    }

    void Update() {
        if (this.timer >= this.delay) {
            this.function();
            this.timer = 0.0f;
            this.CompleteTask();
        }

        this.timer += Time.deltaTime;
    }

    void CompleteTask() {
        if (this.setOnCompleteFunction != null) this.setOnCompleteFunction();
        Destroy(gameObject);
    }

    public Sleep SetOnComplete(Action setOnCompleteFunction) {
        this.setOnCompleteFunction = setOnCompleteFunction;
        return this;
    }

    public static Sleep BeforeFunction(Action function, float delay) {
        Sleep sleepObject = new GameObject("~Sleep").AddComponent<Sleep>();
        sleepObject.Initialise(function, delay);
        return sleepObject;
    }
}