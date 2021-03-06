using System;
using UnityEngine;
using Unity.Mathematics;

public class CursorMovement : MonoBehaviour {
    public static event Action<float2> onVirtualCursorMove;

    [SerializeField] float tweenDuration;

    public void FollowRealCursor(Vector3 realCursorPosition) {
        if (transform.position == realCursorPosition) return;

        LeanTween.cancel(gameObject);
        float3 virtualCursorPosition = Global.Camera.ScreenToWorldPoint(realCursorPosition);
        transform.LeanMoveLocalX(virtualCursorPosition.x, tweenDuration)
                 .setEaseOutExpo();
        transform.LeanMoveLocalY(virtualCursorPosition.y, tweenDuration)
                 .setEaseOutExpo();

        float3 currentVirtualCursorPosition = transform.position;
        CursorMovement.onVirtualCursorMove?.Invoke(currentVirtualCursorPosition.xy);
    }
}