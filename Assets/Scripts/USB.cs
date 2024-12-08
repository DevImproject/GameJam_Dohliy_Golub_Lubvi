using UnityEngine;
using UnityEngine.UI; // Для вывода сообщения в UI

public class USB : MonoBehaviour
{
    public Text messageText; // Ссылка на UI текст, куда будет выводиться сообщение
    public Image messageBG; // Ссылка на UI текст, куда будет выводиться сообщение
    private bool isStuck = false; // Флаг, чтобы отслеживать, застряла ли птица
    public float displayDuration = 3f; // Длительность отображения текста в секундах

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулась ли птица с объектом USB
        if (collision.collider.CompareTag("Bird"))
        {
            // Получаем угол поворота птицы относительно USB
            float angle = Vector3.Angle(collision.transform.up, collision.contacts[0].normal);

            // Если угол больше 45 градусов, птица "застряла" в USB, не той стороной
            if (angle > 45f & !isStuck)
            {
                isStuck = true; // Устанавливаем флаг застревания
                messageBG.gameObject.SetActive(true); // Скрываем текст
                //ShowMessage("Как всегда, USB-штекер повёрнут не той стороной... нужно перевернуть его и воткнуть снова!"); // Показываем сообщение о неправильной ориентации
                StopBird(collision); // Останавливаем движение птицы
          
            }
            else
            {
                //ShowMessage(""); // Убираем сообщение, если птица корректно вставляется
                isStuck = false; // Сбрасываем флаг застревания
                StartCoroutine(HideMessageAfterDelay());
            }
        }
        else
        {
            StartCoroutine(HideMessageAfterDelay());
            //ShowMessage(""); // Убираем сообщение, если птица корректно вставляется
        }
    }

    // Метод для остановки движения птицы при застревании
    private void StopBird(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Останавливаем физику, птица застряла
            rb.velocity = Vector3.zero; // Обнуляем скорость, чтобы птица не двигалась
        }
    }

    // Метод для отображения сообщения на UI
    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message; // Отображаем переданное сообщение
        }
    }

    private System.Collections.IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // Ждем указанное количество секунд
        //ShowMessage(""); // Убираем сообщение, если птица корректно вставляется
        messageBG.gameObject.SetActive(false); // Скрываем текст
    }
}