using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public float smoothSpeed = 5f;

    // Смещение камеры относительно игрока
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        // Если target не назначен — ничего не делаем
        if (target == null)
            return;

        // Желаемая позиция камеры
        Vector3 targetPosition = target.position + offset;

        // Плавное движение камеры
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}