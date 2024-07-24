// ����������� ����������� ������������ ���� ��� ������ � ����� � Unity
using Unity.Netcode;
using UnityEngine;

// ���������� ����� ServerColors, ���������� �� ServerObjectWithIngredientType
// ���� ����� ����� ���� ����������� ��� ���������� ��������� � ������ ������������ �� �������
public class ServerColors : ServerObjectWithIngredientType
{
    // ���� ��� ����� ����, �� ����� ����� �������� �������������� ������, ��������� � �������
}

// ���������� ������������ ��� ����� ������������
public enum IngredientType
{
    red,    // �������
    blue,   // �����
    green,  // ������
    orange, // ���������
    black,  // ׸����
    pink,   // �������
    max     // ������������ ��� ����������� ���������� �����, �� �������� �������� �����
}

// ���������� ����� ServerObjectWithIngredientType, ���������� �� NetworkBehaviour
// ���� ����� ��������� �������� ����� ��� ����������� � ���������������� ��� ����� ����
public class ServerObjectWithIngredientType : NetworkBehaviour
{
    // �������������� ����� OnNetworkSpawn ��� ������������� ������� ��� ��� ��������� � ����
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn(); // �������� ������� ���������� ������
        if (!IsServer) // ���������, ����������� �� ��� �� �������
        {
            enabled = false; // ���� ���, ��������� ���������
            return; // � ������� �� ������
        }
    }

    // ��������� ���������� ���� ��� �������� �������� ���� �����������
    // ��� ��������� ������������� ���������������� �������� ���� ����������� ����� �������� � ���������
    [SerializeField]
    public NetworkVariable<IngredientType> CurrentIngredientType;
}