using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ServerColorsSpawner : NetworkBehaviour
{
    // Список точек спауна
    public List<GameObject> m_SpawnPoints;

    // Частота спауна в секундах
    public float SpawnRatePerSecond;

    // Префаб объекта, который будет спауниться
    public GameObject m_IngredientPrefab;

    private float m_LastSpawnTime;

    // Вызывается при спауне объекта на сети
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        // Если не сервер, отключаем скрипт
        if (!IsServer)
        {
            enabled = false;
            return;
        }
    }

    // Вызывается на каждом кадре
    private void FixedUpdate()
    {
        // Если нет NetworkManager или не сервер, выходим
        if (NetworkManager != null && !IsServer) return;

        // Проверяем прошло ли достаточно времени для спауна
        if (Time.time - m_LastSpawnTime > SpawnRatePerSecond)
        {
            // Проходим по всем точкам спауна
            foreach (var spawnPoint in m_SpawnPoints)
            {
                // Создаем новый объект из префаба на позиции и с ориентацией точки спауна
                var newIngredientObject = Instantiate(m_IngredientPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                // Устанавливаем позицию объекта
                newIngredientObject.transform.position = spawnPoint.transform.position;
                // Получаем компонент ServerColors у нового объекта
                var ingredient = newIngredientObject.GetComponent<ServerColors>();
                // Устанавливаем случайный тип ингредиента
                ingredient.CurrentIngredientType.Value = (IngredientType)Random.Range(0, 7);
                // Спауним объект на сети
                ingredient.NetworkObject.Spawn();
            }
            // Обновляем время последнего спауна
            m_LastSpawnTime = Time.time;
        }
    }
}