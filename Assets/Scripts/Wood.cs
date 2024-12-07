using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
	public GameObject WoodShatter;
    public AudioSource WoodCollision;
    public Toggle toggle; // Ссылка на Toggle для управления разрушением

    private bool canDestroy = true; // Флаг, управляющий возможностью разрушения

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
            WoodCollision.Play();
        }
		if (collision.relativeVelocity.magnitude > 13.5f && canDestroy)
		{
            Destroy();
		}
	}

	private void Destroy()
	{
        GameManager.Instance.WoodDestruction.Play();
        GameObject shatter = Instantiate(WoodShatter, transform.position, Quaternion.identity);
        GameManager.Instance.AddScore(500, transform.position, Color.white);
        Destroy(shatter, 2);
		Destroy(gameObject);
	}

    private void OnToggleValueChanged(bool isOn)
    {
        canDestroy = isOn; // Обновляем состояние разрушения в зависимости от Toggle
        Debug.Log($"Wood destruction is now {(canDestroy ? "enabled" : "disabled")}.");
    }
}