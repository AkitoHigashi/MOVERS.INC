using UnityEngine;

public class MonsterVisionGizmo : MonoBehaviour
{
    [SerializeField, Header("FacePos")]
    private Transform _facePos;

    [SerializeField, Header("視野角の分割数（滑らかさ）")]
    private int _segments = 20;

    [SerializeField, Header("通常時の色")]
    private Color _normalColor = new Color(0, 1, 1, 0.3f); // シアン半透明

    [SerializeField, Header("発見時の色")]
    private Color _detectedColor = new Color(1, 0, 0, 0.3f); // 赤半透明

    private MonsterBase _monsterBase;

    private void Awake()
    {
        _monsterBase = GetComponent<MonsterBase>();
    }

    private void OnDrawGizmos()
    {
        if (_monsterBase == null || _facePos == null) return;

        // 現在の視野距離と視野角を取得
        float fovDistance = _monsterBase.HasSeen ? _monsterBase.PatrolFovDistance : _monsterBase.MonsterFovDistance;
        float fov = _monsterBase.FoV;

        // 色を設定（発見時は赤、通常時はシアン）
        Gizmos.color = _monsterBase.HasSeen ? _detectedColor : _normalColor;

        Vector3 origin = _facePos.position;
        Vector3 forward = transform.forward;

        // 視野角の半分の角度
        float halfFov = fov / 2f;

        // 視野円錐を描画
        DrawVisionCone(origin, forward, fovDistance, halfFov);

        // 中心線を描画
        Gizmos.color = _monsterBase.HasSeen ? Color.red : Color.cyan;
        Gizmos.DrawRay(origin, forward * fovDistance);
    }

    /// <summary>
    /// 視野円錐を描画
    /// </summary>
    private void DrawVisionCone(Vector3 origin, Vector3 forward, float distance, float halfAngle)
    {
        // 前回の点を保持
        Vector3 prevPoint = origin;

        // 円錐の外周を描画
        for (int i = 0; i <= _segments; i++)
        {
            float angle = Mathf.Lerp(-halfAngle, halfAngle, (float)i / _segments);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * forward;
            Vector3 endPoint = origin + direction * distance;

            // 原点から外周への線
            Gizmos.DrawLine(origin, endPoint);

            // 外周同士を繋ぐ線
            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, endPoint);
            }

            prevPoint = endPoint;
        }

        // 垂直方向の視野角も描画（Y軸）
        for (int i = 0; i <= _segments; i++)
        {
            float angle = Mathf.Lerp(-halfAngle, halfAngle, (float)i / _segments);
            Vector3 direction = Quaternion.Euler(angle, 0, 0) * forward;
            Vector3 endPoint = origin + direction * distance;

            // 原点から外周への線
            Gizmos.DrawLine(origin, endPoint);
        }
    }
}