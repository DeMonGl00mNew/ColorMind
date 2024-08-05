using Unity.Netcode; // Импорт библиотеки Unity.Netcode для работы с сетью
using UnityEngine; // Импорт библиотеки UnityEngine для работы с Unity

[RequireComponent(typeof(ServerPlayerMove))] // Требуется компонент ServerPlayerMove
[DefaultExecutionOrder(1)] // Устанавливает порядок выполнения по умолчанию
public class ClientPlayerMove : NetworkBehaviour // Класс для управления движением игрока на клиентской стороне
{
    public float speed = 10.0F; // Скорость движения игрока
    public float rotateSpeed = 2.5F; // Скорость поворота игрока
    public CharacterController CharacterController; // Ссылка на CharacterController

    public Camera m_Camera; // Ссылка на камеру

    private ServerPlayerMove m_Server; // Ссылка на компонент ServerPlayerMove

    private float moveRot, moveForward; // Переменные для управления движением

    private void Awake()
    {
        m_Server = GetComponent<ServerPlayerMove>(); // Получаем компонент ServerPlayerMove при инициализации
    }

    public override void OnNetworkSpawn()
    {
        enabled = IsClient; // Активируем компонент только для клиента
        if (!IsOwner)
        {
            m_Camera.gameObject.SetActive(false); // Отключаем камеру, если игрок не владеет объектом
            enabled = false; // Отключаем компонент
            return;
        }
    }

    [ClientRpc]
    public void SetSpawnClientRpc(Vector3 position)
    {
        if (IsOwner)
        {
            CharacterController.enabled = false; // Отключаем CharacterController
            transform.position = position; // Устанавливаем позицию объекта
            CharacterController.enabled = true; // Включаем CharacterController
            gameObject.SetActive(true); // Активируем объект
        }
    }

    void FixedUpdate()
    {
        if (IsClient && IsOwner)
        {
            transform.Rotate(0, Input.GetAxisRaw("Horizontal") * rotateSpeed, 0); // Поворачиваем объект
            Vector3 forward = transform.TransformDirection(Vector3.forward); // Получаем направление движения
            float curSpeed = speed * Input.GetAxisRaw("Vertical"); // Вычисляем скорость движения
            CharacterController.SimpleMove(forward * curSpeed); // Применяем движение
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_Server.ObjPickedUp.Value)
            {
                m_Server.DropObjServerRpc(); // Вызываем метод сетевого RPC для сброса объекта
            }
            else
            {
                var hit = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("PickupItems"), QueryTriggerInteraction.Ignore); // Проверяем коллизии в радиусе
                if (hit.Length > 0)
                {
                    var ingredient = hit[0].gameObject.GetComponent<ServerColors>(); // Получаем компонент ServerColors
                    if (ingredient != null)
                    {
                        var netObj = ingredient.NetworkObjectId; // Получаем идентификатор сетевого объекта
                        m_Server.PickupObjServerRpc(netObj); // Вызываем метод сетевого RPC для поднятия объекта
                    }
                }
            }
        }
    }
}