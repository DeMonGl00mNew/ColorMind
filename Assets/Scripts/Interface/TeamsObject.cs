using Unity.Netcode; // Импортируем библиотеку Unity Netcode для работы с сетевым кодом.
using TMPro; // Импортируем библиотеку TextMeshPro для работы с текстом.

public class TeamsObject : NetworkBehaviour // Наследуемся от NetworkBehaviour, чтобы использовать функции сетевого взаимодействия.
{
    public int PlayerNumber = 1; // Переменная для хранения номера игрока.
    public NetworkVariable<int> CountPainting = new NetworkVariable<int>(); // Сетевая переменная для хранения количества окрашенных объектов.
    public TMP_Text Score; // Объект TextMeshPro для отображения счета.
    public TMP_Text debug; // Объект TextMeshPro для отладочной информации.

    public override void OnNetworkSpawn() // Метод вызывается, когда объект появляется в сети.
    {
        base.OnNetworkSpawn(); // Вызываем базовую реализацию метода.

        RefreshScoreClientRpc(); // Обновляем счет на клиентских машинах.
    }

    [ClientRpc] // Атрибут, указывающий, что метод должен быть вызван на клиентских машинах.
    public void RefreshScoreClientRpc()
    {
        Score.text = CountPainting.Value.ToString(); // Обновляем текст счета, используя значение CountPainting.
        // debug.text += $" Score.text={Score.text}"; // Добавляем информацию в отладочный текст. Закомментировано, чтобы избежать накопления текста.
    }
}