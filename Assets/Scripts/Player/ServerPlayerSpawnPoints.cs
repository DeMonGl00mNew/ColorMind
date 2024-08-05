// Импорт необходимых пространств имен
using System.Collections.Generic;
using UnityEngine;

// Класс ServerPlayerSpawnPoints, отвечающий за точки спауна игроков на сервере
public class ServerPlayerSpawnPoints : MonoBehaviour
{
    public List<GameObject> m_SpawnPoints; // Список точек спауна игроков
    private static ServerPlayerSpawnPoints s_Instance; // Статический экземпляр класса

    // Статическое свойство Instance, возвращающее единственный экземпляр класса ServerPlayerSpawnPoints
    public static ServerPlayerSpawnPoints Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType<ServerPlayerSpawnPoints>(); // Найти объект ServerPlayerSpawnPoints в сцене
            }

            return s_Instance;
        }
    }

    // Метод вызывается при уничтожении объекта
    private void OnDestroy()
    {
        s_Instance = null; // Обнулить единственный экземпляр класса
    }

    // Метод для получения следующей точки спауна из списка
    public GameObject NextSpawnPoint()
    {
        var toReturn = m_SpawnPoints[m_SpawnPoints.Count - 1]; // Получить последнюю точку спауна
        m_SpawnPoints.RemoveAt(m_SpawnPoints.Count - 1); // Удалить последнюю точку из списка
        return toReturn; // Вернуть полученную точку спауна
    }
}