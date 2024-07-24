using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ServerColorsSpawner : NetworkBehaviour
{
    // ������ ����� ������
    public List<GameObject> m_SpawnPoints;

    // ������� ������ � ��������
    public float SpawnRatePerSecond;

    // ������ �������, ������� ����� ����������
    public GameObject m_IngredientPrefab;

    private float m_LastSpawnTime;

    // ���������� ��� ������ ������� �� ����
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        // ���� �� ������, ��������� ������
        if (!IsServer)
        {
            enabled = false;
            return;
        }
    }

    // ���������� �� ������ �����
    private void FixedUpdate()
    {
        // ���� ��� NetworkManager ��� �� ������, �������
        if (NetworkManager != null && !IsServer) return;

        // ��������� ������ �� ���������� ������� ��� ������
        if (Time.time - m_LastSpawnTime > SpawnRatePerSecond)
        {
            // �������� �� ���� ������ ������
            foreach (var spawnPoint in m_SpawnPoints)
            {
                // ������� ����� ������ �� ������� �� ������� � � ����������� ����� ������
                var newIngredientObject = Instantiate(m_IngredientPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                // ������������� ������� �������
                newIngredientObject.transform.position = spawnPoint.transform.position;
                // �������� ��������� ServerColors � ������ �������
                var ingredient = newIngredientObject.GetComponent<ServerColors>();
                // ������������� ��������� ��� �����������
                ingredient.CurrentIngredientType.Value = (IngredientType)Random.Range(0, 7);
                // ������� ������ �� ����
                ingredient.NetworkObject.Spawn();
            }
            // ��������� ����� ���������� ������
            m_LastSpawnTime = Time.time;
        }
    }
}