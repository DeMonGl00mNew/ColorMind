using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class BallSpawner : NetworkBehaviour
{
    public NetworkObject ballPrefab; // ������ �� ������ ����, ������� ����� ����������
    private float spawnTime = 3; // ����� � �������� ����� �������� �����

    private void Start()
    {
        // ����� ����� ���� �������������, ���� �����������
    }

    private void Update()
    {
        if (IsServer) // ���������, ����������� �� ��� �� �������
        {
            spawnTime -= Time.deltaTime; // ��������� ������ �� ���������� ������
            if (spawnTime <= 0) // ���� ������ ������ 0
            {
                spawnTime = 3; // ���������� ������ �� ���������� ��������
                // �������� RPC ������� ��� ������ ���� �� ������� � ��������� ��������
                SpawnBallServerRpc(new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5)));
            }
        }

    }

    [ServerRpc] // �������, �����������, ��� ����� �������� ��������� ����������, ���������� �� �������
    private void SpawnBallServerRpc(Vector3 spawnPos) // ����� ��� ������ ����
    {
        NetworkObject ballCurrent = Instantiate(ballPrefab, spawnPos, Quaternion.identity); // ������� ��������� ���� � �������� ������� ��� ��������

        ballCurrent.Spawn(); // ������������ ��������� ������ � ����
    }
}