using UnityEngine;
using UnityEngine.UI; // Для вывода сообщения в UI

public class USB : MonoBehaviour
{
    public Text messageText; // Ссылка на UI текст, куда будет выводиться сообщение
    private bool isStuck = false; // Флаг, чтобы отслеживать, застряла ли птица

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
                ShowMessage("Не той стороной переверни"); // Показываем сообщение о неправильной ориентации
                StopBird(collision); // Останавливаем движение птицы
            }
            else
            {
                ShowMessage(""); // Убираем сообщение, если птица корректно вставляется
                isStuck = false; // Сбрасываем флаг застревания
            }
        }
        else
        {
            ShowMessage(""); // Убираем сообщение, если птица корректно вставляется
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
}