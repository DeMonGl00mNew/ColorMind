using Unity.Netcode; // ������ ���������� Unity.Netcode ��� ������ � �����
using UnityEngine; // ������ ���������� UnityEngine ��� ������ � Unity

[RequireComponent(typeof(ServerPlayerMove))] // ��������� ��������� ServerPlayerMove
[DefaultExecutionOrder(1)] // ������������� ������� ���������� �� ���������
public class ClientPlayerMove : NetworkBehaviour // ����� ��� ���������� ��������� ������ �� ���������� �������
{
    public float speed = 10.0F; // �������� �������� ������
    public float rotateSpeed = 2.5F; // �������� �������� ������
    public CharacterController CharacterController; // ������ �� CharacterController

    public Camera m_Camera; // ������ �� ������

    private ServerPlayerMove m_Server; // ������ �� ��������� ServerPlayerMove

    private float moveRot, moveForward; // ���������� ��� ���������� ���������

    private void Awake()
    {
        m_Server = GetComponent<ServerPlayerMove>(); // �������� ��������� ServerPlayerMove ��� �������������
    }

    public override void OnNetworkSpawn()
    {
        enabled = IsClient; // ���������� ��������� ������ ��� �������
        if (!IsOwner)
        {
            m_Camera.gameObject.SetActive(false); // ��������� ������, ���� ����� �� ������� ��������
            enabled = false; // ��������� ���������
            return;
        }
    }

    [ClientRpc]
    public void SetSpawnClientRpc(Vector3 position)
    {
        if (IsOwner)
        {
            CharacterController.enabled = false; // ��������� CharacterController
            transform.position = position; // ������������� ������� �������
            CharacterController.enabled = true; // �������� CharacterController
            gameObject.SetActive(true); // ���������� ������
        }
    }

    void FixedUpdate()
    {
        if (IsClient && IsOwner)
        {
            transform.Rotate(0, Input.GetAxisRaw("Horizontal") * rotateSpeed, 0); // ������������ ������
            Vector3 forward = transform.TransformDirection(Vector3.forward); // �������� ����������� ��������
            float curSpeed = speed * Input.GetAxisRaw("Vertical"); // ��������� �������� ��������
            CharacterController.SimpleMove(forward * curSpeed); // ��������� ��������
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_Server.ObjPickedUp.Value)
            {
                m_Server.DropObjServerRpc(); // �������� ����� �������� RPC ��� ������ �������
            }
            else
            {
                var hit = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("PickupItems"), QueryTriggerInteraction.Ignore); // ��������� �������� � �������
                if (hit.Length > 0)
                {
                    var ingredient = hit[0].gameObject.GetComponent<ServerColors>(); // �������� ��������� ServerColors
                    if (ingredient != null)
                    {
                        var netObj = ingredient.NetworkObjectId; // �������� ������������� �������� �������
                        m_Server.PickupObjServerRpc(netObj); // �������� ����� �������� RPC ��� �������� �������
                    }
                }
            }
        }
    }
}