using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public UnityTransport transport; // Объявление переменной для сетевого транспорта
    public TMP_InputField ServerInput; // Поле ввода для адреса сервера
    public TMP_InputField PortInput; // Поле ввода для порта

    // Метод для инициализации хоста
    public void Host()
    {
        // Установка данных соединения (адрес и порт) из полей ввода
        transport.SetConnectionData(ServerInput.text, ushort.Parse(PortInput.text), null);
        // Запуск хоста
        NetworkManager.Singleton.StartHost();
    }

    // Метод для подключения клиента
    public void Client()
    {
        // Установка данных соединения (адрес и порт) из полей ввода
        transport.SetConnectionData(ServerInput.text, ushort.Parse(PortInput.text), null);
        // Запуск клиента
        NetworkManager.Singleton.StartClient();
    }
}