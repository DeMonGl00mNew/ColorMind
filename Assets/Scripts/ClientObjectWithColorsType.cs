using UnityEngine;
using Unity.Netcode;

public class ClientObjectWithColorsType : NetworkBehaviour
{
    // Ссылка на серверный объект с типом ингредиента
    private ServerObjectWithIngredientType m_Server;
    // Ссылка на компонент Renderer
    private Renderer m_Renderer;

    private void Awake()
    {
        // Получаем ссылки на компоненты при инициализации
        m_Server = GetComponent<ServerObjectWithIngredientType>();
        m_Renderer = GetComponent<Renderer>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        // Включаем объект только на клиенте
        enabled = IsClient;
    }

    // Метод для обновления материала объекта в зависимости от типа ингредиента
    void UpdateMaterial()
    {
        switch (m_Server.CurrentIngredientType.Value)
        {
            case IngredientType.black:
                m_Renderer.material.color = ColorManager.Instance.teamColours[4];
                break;
            case IngredientType.blue:
                m_Renderer.material.color = ColorManager.Instance.teamColours[1];
                break;
            case IngredientType.green:
                m_Renderer.material.color = ColorManager.Instance.teamColours[2];
                break;
            case IngredientType.orange:
                m_Renderer.material.color = ColorManager.Instance.teamColours[3];
                break;
            case IngredientType.pink:
                m_Renderer.material.color = ColorManager.Instance.teamColours[5];
                break;
            case IngredientType.red:
                m_Renderer.material.color = ColorManager.Instance.teamColours[0];
                break;
        }
    }

    public void Update()
    {
        // Вызываем метод обновления материала каждый кадр
        UpdateMaterial();
    }
}