// Импорт библиотеки Unity.Netcode для работы с сетью
using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine; // Импорт библиотеки UnityEngine для работы с Unity

[DefaultExecutionOrder(0)] // Устанавливает порядок выполнения по умолчанию
public class ServerPlayerMove : NetworkBehaviour // Класс для управления движением игрока на серверной стороне
{
    private NetworkObject m_PickedUpObj; // Ссылка на поднятый объект
    private ClientPlayerMove m_Client; // Ссылка на компонент ClientPlayerMove

    [SerializeField]
    private Camera m_Camera; // Ссылка на камеру

    public NetworkVariable<bool> ObjPickedUp = new NetworkVariable<bool>(); // Переменная для отслеживания поднятого объекта

    private void Awake()
    {
        m_Client = GetComponent < ClientPlayerMove>(); // Получаем компонент ClientPlayerMove при инициализации
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsServer)
        {
            enabled = false; // Отключаем компонент, если это не сервер
            return;
        }

        var spawnPoint = ServerPlayerSpawnPoints.Instance.NextSpawnPoint(); // Получаем точку спауна для игрока
        m_Client.SetSpawnClientRpc(spawnPoint.transform.position); // Устанавливаем позицию спауна на клиенте
    }

    [ServerRpc]
    public void PickupObjServerRpc(ulong objToPickupID)
    {
        NetworkManager.SpawnManager.SpawnedObjects.TryGetValue(objToPickupID, out var objToPickup); // Получаем объект для поднятия по идентификатору
        if (objToPickup == null || objToPickup.transform.parent != null) return; // Проверяем условия для поднятия объекта

        objToPickup.GetComponent<Rigidbody>().isKinematic = true; // Делаем объект кинематическим
        objToPickup.transform.parent = transform; // Устанавливаем объект как дочерний
        objToPickup.GetComponent<NetworkTransform>().InLocalSpace = true; // Устанавливаем локальное пространство
        objToPickup.transform.localPosition = Vector3.up; // Устанавливаем позицию объекта
        ObjPickedUp.Value = true; // Устанавливаем флаг поднятия объекта
        m_PickedUpObj = objToPickup; // Сохраняем ссылку на поднятый объект
    }

    [ServerRpc]
    public void DropObjServerRpc()
    {
        if (m_PickedUpObj != null)
        {
            m_PickedUpObj.transform.localPosition = new Vector3(0, 0, 2); // Устанавливаем позицию для сброса объекта
            m_PickedUpObj.transform.parent = null; // Убираем объект из родительского объекта
            m_PickedUpObj.GetComponent<Rigidbody>().isKinematic = false; // Делаем объект не кинематическим
            m_PickedUpObj.GetComponent<NetworkTransform>().InLocalSpace = false; // Устанавливаем глобальное пространство
            m_PickedUpObj = null; // Сбрасываем ссылку на поднятый объект
        }

        ObjPickedUp.Value = false; // Сбрасываем флаг поднятия объекта
    }
}