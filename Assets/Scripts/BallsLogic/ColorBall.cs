using UnityEngine;
using Unity.Netcode;

public class ColorBall : NetworkBehaviour
{
    // ������ �� �������� ����
    public Renderer ballRenderer;

    // ���������� ��� �������� ����� ���� � ���������� ����
    private NetworkVariable<Color> ballCollor = new NetworkVariable<Color>();

    // ����� ���������� ��� ��������� ������� �� �����
    public override void OnNetworkSpawn()
    {
        // ���� �� ������, �������
        if (!IsServer) return;

        // ������������� ��������� ���� ����
        ballCollor.Value = UnityEngine.Random.ColorHSV();
    }

    // ����� ���������� ��� ��������� �������
    private void OnEnable()
    {
        // ������������� �� ������� ��������� ����� ����
        ballCollor.OnValueChanged += OnBallColourChanged;
    }

    // ����� ���������� ��� ���������� �������
    private void OnDisable()
    {
        // ������������ �� ������� ��������� ����� ����
        ballCollor.OnValueChanged -= OnBallColourChanged;
    }

    // ����� ���������� ��� ��������� ����� ����
    private void OnBallColourChanged(Color previousColor, Color newColor)
    {
        // ���� �� ������, �������
        if (!IsClient) return;

        // ������������� ����� ���� ��������� ��������� ����
        ballRenderer.material.color = newColor;
    }
}