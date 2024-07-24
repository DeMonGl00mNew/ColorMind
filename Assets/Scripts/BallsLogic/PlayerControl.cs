using UnityEngine;
using Unity.Netcode;

public class PlayerControl : NetworkBehaviour
{
    public float speed = 10.0f; // �������� �������� ������
    public float rotationSpeed = 100.0f; // �������� �������� ������
    private float moveRot, moveForward; // ���������� ��� �������� �������� �������� � ��������

    private void Update()
    {
        if (IsClient && IsOwner)
        {
            moveForward = Input.GetAxis("Vertical") * Time.deltaTime; // �������� �������� ������/�����
            moveRot = Input.GetAxis("Horizontal") * Time.deltaTime; // �������� �������� �����/������

            transform.Translate(0, 0, moveForward * speed); // ������� ������ ������/�����
            transform.Rotate(0, moveRot * rotationSpeed, 0); // ������������ ������
        }
    }
}