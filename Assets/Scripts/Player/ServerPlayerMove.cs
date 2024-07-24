// ������ ���������� Unity.Netcode ��� ������ � �����
using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine; // ������ ���������� UnityEngine ��� ������ � Unity

[DefaultExecutionOrder(0)] // ������������� ������� ���������� �� ���������
public class ServerPlayerMove : NetworkBehaviour // ����� ��� ���������� ��������� ������ �� ��������� �������
{
    private NetworkObject m_PickedUpObj; // ������ �� �������� ������
    private ClientPlayerMove m_Client; // ������ �� ��������� ClientPlayerMove

    [SerializeField]
    private Camera m_Camera; // ������ �� ������

    public NetworkVariable<bool> ObjPickedUp = new NetworkVariable<bool>(); // ���������� ��� ������������ ��������� �������

    private void Awake()
    {
        m_Client = GetComponent < ClientPlayerMove>(); // �������� ��������� ClientPlayerMove ��� �������������
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsServer)
        {
            enabled = false; // ��������� ���������, ���� ��� �� ������
            return;
        }

        var spawnPoint = ServerPlayerSpawnPoints.Instance.NextSpawnPoint(); // �������� ����� ������ ��� ������
        m_Client.SetSpawnClientRpc(spawnPoint.transform.position); // ������������� ������� ������ �� �������
    }

    [ServerRpc]
    public void PickupObjServerRpc(ulong objToPickupID)
    {
        NetworkManager.SpawnManager.SpawnedObjects.TryGetValue(objToPickupID, out var objToPickup); // �������� ������ ��� �������� �� ��������������
        if (objToPickup == null || objToPickup.transform.parent != null) return; // ��������� ������� ��� �������� �������

        objToPickup.GetComponent<Rigidbody>().isKinematic = true; // ������ ������ ��������������
        objToPickup.transform.parent = transform; // ������������� ������ ��� ��������
        objToPickup.GetComponent<NetworkTransform>().InLocalSpace = true; // ������������� ��������� ������������
        objToPickup.transform.localPosition = Vector3.up; // ������������� ������� �������
        ObjPickedUp.Value = true; // ������������� ���� �������� �������
        m_PickedUpObj = objToPickup; // ��������� ������ �� �������� ������
    }

    [ServerRpc]
    public void DropObjServerRpc()
    {
        if (m_PickedUpObj != null)
        {
            m_PickedUpObj.transform.localPosition = new Vector3(0, 0, 2); // ������������� ������� ��� ������ �������
            m_PickedUpObj.transform.parent = null; // ������� ������ �� ������������� �������
            m_PickedUpObj.GetComponent<Rigidbody>().isKinematic = false; // ������ ������ �� ��������������
            m_PickedUpObj.GetComponent<NetworkTransform>().InLocalSpace = false; // ������������� ���������� ������������
            m_PickedUpObj = null; // ���������� ������ �� �������� ������
        }

        ObjPickedUp.Value = false; // ���������� ���� �������� �������
    }
}