using UnityEngine;
using Unity.Netcode;

public class ColorBall : NetworkBehaviour
{
    // Ссылка на рендерер шара
    public Renderer ballRenderer;

    // Переменная для хранения цвета шара с поддержкой сети
    private NetworkVariable<Color> ballCollor = new NetworkVariable<Color>();

    // Метод вызывается при появлении объекта на сцене
    public override void OnNetworkSpawn()
    {
        // Если не сервер, выходим
        if (!IsServer) return;

        // Устанавливаем случайный цвет шару
        ballCollor.Value = UnityEngine.Random.ColorHSV();
    }

    // Метод вызывается при включении объекта
    private void OnEnable()
    {
        // Подписываемся на событие изменения цвета шара
        ballCollor.OnValueChanged += OnBallColourChanged;
    }

    // Метод вызывается при выключении объекта
    private void OnDisable()
    {
        // Отписываемся от события изменения цвета шара
        ballCollor.OnValueChanged -= OnBallColourChanged;
    }

    // Метод вызывается при изменении цвета шара
    private void OnBallColourChanged(Color previousColor, Color newColor)
    {
        // Если не клиент, выходим
        if (!IsClient) return;

        // Устанавливаем новый цвет материалу рендерера шара
        ballRenderer.material.color = newColor;
    }
}