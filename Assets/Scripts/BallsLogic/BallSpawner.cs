using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class BallSpawner : NetworkBehaviour
{
    public NetworkObject ballPrefab; // Ссылка на префаб шара, который будет спауниться
    private float spawnTime = 3; // Время в секундах между спаунами шаров

    private void Start()
    {
        // Здесь может быть инициализация, если потребуется
    }

    private void Update()
    {
        if (IsServer) // Проверяем, выполняется ли код на сервере
        {
            spawnTime -= Time.deltaTime; // Уменьшаем таймер до следующего спауна
            if (spawnTime <= 0) // Если таймер достиг 0
            {
                spawnTime = 3; // Сбрасываем таймер до начального значения
                // Вызываем RPC функцию для спауна шара на сервере с рандомной позицией
                SpawnBallServerRpc(new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5)));
            }
        }

    }

    [ServerRpc] // Атрибут, указывающий, что метод является удаленной процедурой, вызываемой на сервере
    private void SpawnBallServerRpc(Vector3 spawnPos) // Метод для спауна шара
    {
        NetworkObject ballCurrent = Instantiate(ballPrefab, spawnPos, Quaternion.identity); // Создаем экземпляр шара в заданной позиции без вращения

        ballCurrent.Spawn(); // Регистрируем созданный объект в сети
    }
}