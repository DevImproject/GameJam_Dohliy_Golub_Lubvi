using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class StartMessage : MonoBehaviour
{
    public Text messageText; // UI текст для отображения сообщения
    public string startMessage = "Любви покорны все, особенно мёртвые голуби. Этот прекрасный мёртвый птиц готов преодолеть любые препятствия, чтобы вонзиться своим USB-клювом в порт назначения, так поможем же ему!"; // Текст сообщения
    public float displayDuration = 5f; // Длительность отображения текста в секундах

    private void Start()
    {
        if (messageText != null)
        {
            // Устанавливаем текст сообщения
            messageText.text = startMessage;

            // Запускаем корутину для скрытия текста через 5 секунд
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private System.Collections.IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // Ждем указанное количество секунд

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // Скрываем текст
        }

        // Игра продолжается, можно добавить дополнительную логику здесь
    }
}