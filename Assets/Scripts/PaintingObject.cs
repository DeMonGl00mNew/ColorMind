using UnityEngine;
using Unity.Netcode;
public class PaintingObject : ServerObjectWithIngredientType
{

    public TeamsObject teamObjects;
    private Component[] childComponents;


    // Метод вызывается при появлении объекта в сети
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        // Если не клиент, выходим из метода
        if (!IsClient)
        {
            return;
        }
        // Получаем компоненты ServerColors у дочерних объектов
        childComponents = GetComponentsInChildren<ServerColors>();
    }

    // Метод вызывается при столкновении с другим коллайдером
    private void OnTriggerEnter(Collider other)
    {
        // Если не сервер, выходим из метода
        if (!IsServer) return;
        // Получаем компоненты ServerColors у дочерних объектов
        childComponents = GetComponentsInChildren<ServerColors>();
        // Получаем компонент ServerColors у другого объекта
        var ingredient = other.gameObject.GetComponent<ServerColors>();

        // Если компонент отсутствует или массив дочерних компонентов пуст, выходим из метода
        if (ingredient == null || childComponents.Length == 0)
        {
            return;
        }

        // Перебираем все дочерние компоненты
        foreach (ServerColors component in childComponents)
        {
            // Если тип ингредиента совпадает
            if (component.CurrentIngredientType.Value == ingredient.CurrentIngredientType.Value)
            {
                // Вызываем метод RenderColorObjectClientRpc для изменения цвета
                RenderColorObjectClientRpc((int)component.gameObject.transform.GetSiblingIndex(),
                                           (int)component.CurrentIngredientType.Value);

                // Уменьшаем счетчик объектов для рисования
                teamObjects.CountPainting.Value -= 1;
                // Если владелец объекта, обновляем счет команды
                if (IsOwner)
                {
                    teamObjects.RefreshScoreClientRpc();
                }

                // Устанавливаем новый тип ингредиента и уничтожаем другой объект
                component.CurrentIngredientType.Value = IngredientType.max;
                ingredient.NetworkObject.Despawn(destroy: true);
                return;
            }
        }
    }

    // RPC-метод для изменения цвета объекта
    [ClientRpc]
    private void RenderColorObjectClientRpc(int index, int colorIndex)
    {
        childComponents[index].gameObject.GetComponent<Renderer>().material.color = ColorManager.Instance.teamColours[colorIndex];
    }
}