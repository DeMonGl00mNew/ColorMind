using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс ColorManager, отвечающий за управление цветами в игре
public class ColorManager : MonoBehaviour
{
    static public ColorManager Instance { get; private set; } // Статическое свойство для доступа к единственному экземпляру класса
    public Color[] teamColours; // Массив цветов для команд

    // Метод, вызываемый при создании объекта
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Установить текущий объект как единственный экземпляр

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Уничтожить объект, если уже существует другой экземпляр класса
        }
    }
}