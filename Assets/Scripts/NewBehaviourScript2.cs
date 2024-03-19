using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera freeLookCamera;
     // Ссылка на компонент CinemachineFreeLook
    public float minFOV = 20f; // Минимальное поле зрения
    public float maxFOV = 60f; // Максимальное поле зрения
    public float minSpeed = 1f; // Минимальная скорость для максимального FOV
    public float maxSpeed = 10f; // Максимальная скорость для минимального FOV

    private void Update()
    {
        float speed =transform.GetComponent<Rigidbody>().velocity.magnitude; // Ваш код для получения текущей скорости игрока (например, playerRigidbody.velocity.magnitude)

        // Интерполяция между minFOV и maxFOV в зависимости от скорости
        float targetFOV = Mathf.Lerp(maxFOV, minFOV, Mathf.InverseLerp(minSpeed, maxSpeed, speed));

        // Устанавливаем поле зрения в CinemachineFreeLook
        freeLookCamera.m_Lens.FieldOfView = targetFOV;
    }
}
