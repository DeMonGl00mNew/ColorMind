// ������ ����������� ����������� ����
using System.Collections.Generic;
using UnityEngine;

// ����� ServerPlayerSpawnPoints, ���������� �� ����� ������ ������� �� �������
public class ServerPlayerSpawnPoints : MonoBehaviour
{
    public List<GameObject> m_SpawnPoints; // ������ ����� ������ �������
    private static ServerPlayerSpawnPoints s_Instance; // ����������� ��������� ������

    // ����������� �������� Instance, ������������ ������������ ��������� ������ ServerPlayerSpawnPoints
    public static ServerPlayerSpawnPoints Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType<ServerPlayerSpawnPoints>(); // ����� ������ ServerPlayerSpawnPoints � �����
            }

            return s_Instance;
        }
    }

    // ����� ���������� ��� ����������� �������
    private void OnDestroy()
    {
        s_Instance = null; // �������� ������������ ��������� ������
    }

    // ����� ��� ��������� ��������� ����� ������ �� ������
    public GameObject NextSpawnPoint()
    {
        var toReturn = m_SpawnPoints[m_SpawnPoints.Count - 1]; // �������� ��������� ����� ������
        m_SpawnPoints.RemoveAt(m_SpawnPoints.Count - 1); // ������� ��������� ����� �� ������
        return toReturn; // ������� ���������� ����� ������
    }
}