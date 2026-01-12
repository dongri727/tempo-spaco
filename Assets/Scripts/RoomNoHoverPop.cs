using UnityEngine;
using TMPro; // TextMeshPro使ってないなら消してOK
using UnityEngine.UI; // UI Text使ってないなら消してOK

public class RoomNoHoverPop : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public RectTransform roomNoRect;      // roomNo の RectTransform（UIなら必ずある）
    public CanvasGroup roomNoCanvasGroup; // 付いてなければroomNoに追加
    public Camera cam;                    // 省略可（nullならMainCameraを使う）

    [Tooltip("Raycast will only hit these layers. Set to your 'Hover' layer for stability.")]
    public LayerMask hoverLayerMask = ~0; // default: everything

    [Tooltip("Log when hover state changes (useful for debugging).")]
    public bool debugLogHover = false;

    [Header("Animation")]
    public float popScale = 1.15f;
    public float animTime = 0.12f;

    Vector3 baseScale;
    bool isHover;
    float t; // 0..1

    void Reset()
    {
        cam = Camera.main;
    }

    void Awake()
    {
        if (cam == null) cam = Camera.main;

        // 安全策：CanvasGroupが未設定ならroomNo側から拾う
        if (roomNoCanvasGroup == null && roomNoRect != null)
            roomNoCanvasGroup = roomNoRect.GetComponent<CanvasGroup>();

        if (roomNoRect != null)
            baseScale = roomNoRect.localScale;

        // 初期は非表示
        if (roomNoCanvasGroup != null) roomNoCanvasGroup.alpha = 0f;
        if (roomNoRect != null) roomNoRect.localScale = baseScale * 0.9f;
    }

    void Update()
    {
        // --- hover判定（Raycast）---
        bool nowHover = IsMouseOverThisObject();

        if (debugLogHover && nowHover != isHover)
            Debug.Log($"[RoomNoHoverPop] Hover={(nowHover ? "ON" : "OFF")} on {name}");

        if (nowHover != isHover)
        {
            isHover = nowHover;
            // 方向を切り替える時にtを反転させると、途中から自然に戻る
            t = 1f - t;
        }

        // --- アニメーション更新 ---
        float dir = isHover ? 1f : -1f;
        t = Mathf.Clamp01(t + (Time.unscaledDeltaTime / animTime) * dir);

        // easeOutっぽい補間（適度に気持ちいい）
        float eased = 1f - Mathf.Pow(1f - t, 3f);

        if (roomNoCanvasGroup != null)
            roomNoCanvasGroup.alpha = eased;

        if (roomNoRect != null)
        {
            float s = Mathf.Lerp(0.9f, popScale, eased);
            roomNoRect.localScale = baseScale * s;
        }
    }

    bool IsMouseOverThisObject()
    {
        if (cam == null) return false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, hoverLayerMask))
        {
            // compus自体 or 子オブジェクトに当たってもOKにする
            return hit.transform == transform || hit.transform.IsChildOf(transform) || transform.IsChildOf(hit.transform);
        }
        return false;
    }
}