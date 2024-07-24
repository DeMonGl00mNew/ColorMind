using UnityEngine;
using Unity.Netcode;
public class PaintingObject : ServerObjectWithIngredientType
{

    public TeamsObject teamObjects;
    private Component[] childComponents;


    // ����� ���������� ��� ��������� ������� � ����
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        // ���� �� ������, ������� �� ������
        if (!IsClient)
        {
            return;
        }
        // �������� ���������� ServerColors � �������� ��������
        childComponents = GetComponentsInChildren<ServerColors>();
    }

    // ����� ���������� ��� ������������ � ������ �����������
    private void OnTriggerEnter(Collider other)
    {
        // ���� �� ������, ������� �� ������
        if (!IsServer) return;
        // �������� ���������� ServerColors � �������� ��������
        childComponents = GetComponentsInChildren<ServerColors>();
        // �������� ��������� ServerColors � ������� �������
        var ingredient = other.gameObject.GetComponent<ServerColors>();

        // ���� ��������� ����������� ��� ������ �������� ����������� ����, ������� �� ������
        if (ingredient == null || childComponents.Length == 0)
        {
            return;
        }

        // ���������� ��� �������� ����������
        foreach (ServerColors component in childComponents)
        {
            // ���� ��� ����������� ���������
            if (component.CurrentIngredientType.Value == ingredient.CurrentIngredientType.Value)
            {
                // �������� ����� RenderColorObjectClientRpc ��� ��������� �����
                RenderColorObjectClientRpc((int)component.gameObject.transform.GetSiblingIndex(),
                                           (int)component.CurrentIngredientType.Value);

                // ��������� ������� �������� ��� ���������
                teamObjects.CountPainting.Value -= 1;
                // ���� �������� �������, ��������� ���� �������
                if (IsOwner)
                {
                    teamObjects.RefreshScoreClientRpc();
                }

                // ������������� ����� ��� ����������� � ���������� ������ ������
                component.CurrentIngredientType.Value = IngredientType.max;
                ingredient.NetworkObject.Despawn(destroy: true);
                return;
            }
        }
    }

    // RPC-����� ��� ��������� ����� �������
    [ClientRpc]
    private void RenderColorObjectClientRpc(int index, int colorIndex)
    {
        childComponents[index].gameObject.GetComponent<Renderer>().material.color = ColorManager.Instance.teamColours[colorIndex];
    }
}