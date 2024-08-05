using UnityEngine;
using Unity.Netcode;

public class PlayerControl : NetworkBehaviour
{
    public float speed = 10.0f; // Скорость движения игрока
    public float rotationSpeed = 100.0f; // Скорость поворота игрока
    private float moveRot, moveForward; // Переменные для хранения значений вращения и движения

    private void Update()
    {
        if (IsClient && IsOwner)
        {
            moveForward = Input.GetAxis("Vertical") * Time.deltaTime; // Получаем значение вперед/назад
            moveRot = Input.GetAxis("Horizontal") * Time.deltaTime; // Получаем значение влево/вправо

            transform.Translate(0, 0, moveForward * speed); // Двигаем игрока вперед/назад
            transform.Rotate(0, moveRot * rotationSpeed, 0); // Поворачиваем игрока
        }
    }
}