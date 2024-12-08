using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
	public GameObject WoodShatter;
    public AudioSource WoodCollision;
    public Toggle toggle; // Ссылка на Toggle для управления разрушением

    private bool canDestroy = true; // Флаг, управляющий возможностью разрушения
    private bool isStuck = false; // Флаг, чтобы узнать, застряла ли птица в дереве

    void Start()
    {
        if (toggle != null)
        {
            // Привязка события изменения состояния Toggle
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            canDestroy = toggle.isOn; // Устанавливаем начальное состояние
        }
    }

    void OnDestroy()
    {
        // Убираем слушатель при уничтожении объекта
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }

    void OnCollisionEnter(Collision collision)
	{
        if (collision.collider.CompareTag("Bird"))
        {
            if (WoodCollision != null)
            {
                WoodCollision.Play();
        }
        }

        float collisionForce = collision.relativeVelocity.magnitude; // Сила столкновения

        //Debug.Log(collisionForce);

        // Проверка застревания птицы в дереве (сила столкновения от 8 до 13.5)
        if (collisionForce >= 8f && collisionForce <= 16.5f && !isStuck)
        {
            isStuck = true;
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Останавливаем физику, чтобы птица застряла
            }

            // Создаем эффект разрушения дерева и воспроизводим звуки
            if (WoodShatter) { 
            GameObject shatter = Instantiate(WoodShatter, transform.position, Quaternion.identity);
            Destroy(shatter, 10f); // Уничтожаем эффект через 2 секунды
            }
            if (WoodCollision != null)
            {
                WoodCollision.Play(); // Проигрываем звук столкновения дерева
        }
        }
        // Если сила столкновения меньше 8, птица отскакивает
        else if (collisionForce < 8f)
        {
            // Птица отскакивает
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Включаем физику обратно
                rb.AddForce(collision.contacts[0].normal * 5f, ForceMode.Impulse); // Отскок от дерева
            }
        }

        // Условие для разрушения дерева
        if (collisionForce > 16.5f && canDestroy)
		{
            Destroy();
		}
	}

	private void Destroy()
	{
        GameManager.Instance.WoodDestruction.Play();
        if (WoodShatter) { 
        GameObject shatter = Instantiate(WoodShatter, transform.position, Quaternion.identity);
        //GameManager.Instance.AddScore(500, transform.position, Color.white);
        Destroy(shatter, 2);
        }
        Destroy(gameObject);
	}

    private void OnToggleValueChanged(bool isOn)
    {
        canDestroy = isOn; // Обновляем состояние разрушения в зависимости от Toggle
        Debug.Log($"Wood destruction is now {(canDestroy ? "enabled" : "disabled")}.");
    }
}