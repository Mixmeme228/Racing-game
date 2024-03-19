using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera freeLookCamera;
     // ������ �� ��������� CinemachineFreeLook
    public float minFOV = 20f; // ����������� ���� ������
    public float maxFOV = 60f; // ������������ ���� ������
    public float minSpeed = 1f; // ����������� �������� ��� ������������� FOV
    public float maxSpeed = 10f; // ������������ �������� ��� ������������ FOV

    private void Update()
    {
        float speed =transform.GetComponent<Rigidbody>().velocity.magnitude; // ��� ��� ��� ��������� ������� �������� ������ (��������, playerRigidbody.velocity.magnitude)

        // ������������ ����� minFOV � maxFOV � ����������� �� ��������
        float targetFOV = Mathf.Lerp(maxFOV, minFOV, Mathf.InverseLerp(minSpeed, maxSpeed, speed));

        // ������������� ���� ������ � CinemachineFreeLook
        freeLookCamera.m_Lens.FieldOfView = targetFOV;
    }
}
