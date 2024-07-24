using Unity.Netcode; // ����������� ���������� Unity Netcode ��� ������ � ������� �����.
using TMPro; // ����������� ���������� TextMeshPro ��� ������ � �������.

public class TeamsObject : NetworkBehaviour // ����������� �� NetworkBehaviour, ����� ������������ ������� �������� ��������������.
{
    public int PlayerNumber = 1; // ���������� ��� �������� ������ ������.
    public NetworkVariable<int> CountPainting = new NetworkVariable<int>(); // ������� ���������� ��� �������� ���������� ���������� ��������.
    public TMP_Text Score; // ������ TextMeshPro ��� ����������� �����.
    public TMP_Text debug; // ������ TextMeshPro ��� ���������� ����������.

    public override void OnNetworkSpawn() // ����� ����������, ����� ������ ���������� � ����.
    {
        base.OnNetworkSpawn(); // �������� ������� ���������� ������.

        RefreshScoreClientRpc(); // ��������� ���� �� ���������� �������.
    }

    [ClientRpc] // �������, �����������, ��� ����� ������ ���� ������ �� ���������� �������.
    public void RefreshScoreClientRpc()
    {
        Score.text = CountPainting.Value.ToString(); // ��������� ����� �����, ��������� �������� CountPainting.
        // debug.text += $" Score.text={Score.text}"; // ��������� ���������� � ���������� �����. ����������������, ����� �������� ���������� ������.
    }
}