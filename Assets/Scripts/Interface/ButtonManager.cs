using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public UnityTransport transport; // ���������� ���������� ��� �������� ����������
    public TMP_InputField ServerInput; // ���� ����� ��� ������ �������
    public TMP_InputField PortInput; // ���� ����� ��� �����

    // ����� ��� ������������� �����
    public void Host()
    {
        // ��������� ������ ���������� (����� � ����) �� ����� �����
        transport.SetConnectionData(ServerInput.text, ushort.Parse(PortInput.text), null);
        // ������ �����
        NetworkManager.Singleton.StartHost();
    }

    // ����� ��� ����������� �������
    public void Client()
    {
        // ��������� ������ ���������� (����� � ����) �� ����� �����
        transport.SetConnectionData(ServerInput.text, ushort.Parse(PortInput.text), null);
        // ������ �������
        NetworkManager.Singleton.StartClient();
    }
}