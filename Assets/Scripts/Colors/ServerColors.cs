// Импортируем необходимые пространства имен для работы с сетью и Unity
using Unity.Netcode;
using UnityEngine;

// Определяем класс ServerColors, наследуясь от ServerObjectWithIngredientType
// Этот класс может быть использован для управления объектами с типами ингредиентов на сервере
public class ServerColors : ServerObjectWithIngredientType
{
    // Пока что класс пуст, но здесь можно добавить дополнительную логику, связанную с цветами
}

// Определяем перечисление для типов ингредиентов
public enum IngredientType
{
    red,    // Красный
    blue,   // Синий
    green,  // Зелёный
    orange, // Оранжевый
    black,  // Чёрный
    pink,   // Розовый
    max     // Используется для определения количества типов, не является реальным типом
}

// Определяем класс ServerObjectWithIngredientType, наследуясь от NetworkBehaviour
// Этот класс позволяет объектам иметь тип ингредиента и синхронизировать его через сеть
public class ServerObjectWithIngredientType : NetworkBehaviour
{
    // Переопределяем метод OnNetworkSpawn для инициализации объекта при его появлении в сети
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn(); // Вызываем базовую реализацию метода
        if (!IsServer) // Проверяем, выполняется ли код на сервере
        {
            enabled = false; // Если нет, отключаем компонент
            return; // И выходим из метода
        }
    }

    // Объявляем переменную сети для хранения текущего типа ингредиента
    // Это позволяет автоматически синхронизировать значение типа ингредиента между сервером и клиентами
    [SerializeField]
    public NetworkVariable<IngredientType> CurrentIngredientType;
}