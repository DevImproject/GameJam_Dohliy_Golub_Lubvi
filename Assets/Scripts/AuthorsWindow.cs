using UnityEngine;
using UnityEngine.UI;

public class AuthorsWindow : MonoBehaviour
{
    public Image authorsPanel; // Панель, где находится окно авторов
    public Button toggleButton;
    private bool isMuted = false; // Флаг для проверки состояния звука

    void Start()
    {
        // Убедитесь, что окно авторов скрыто в начале
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleAvtor);
        }
    }

    private void ToggleAvtor()
    {
        isMuted = !isMuted; // Меняем состояние звука

        if (isMuted)
        {
            authorsPanel.gameObject.SetActive(true);
        }
        else
        {
            authorsPanel.gameObject.SetActive(false); // Возобновляем воспроизведение
        }
    }

    // Метод для показа окна авторов
    public void ShowAuthors()
    {
        if (authorsPanel != null)
        {
            authorsPanel.gameObject.SetActive(true); // Показываем окно
        }
    }

    // Метод для скрытия окна авторов
    public void HideAuthors()
    {
        if (authorsPanel != null)
        {
            authorsPanel.gameObject.SetActive(false); // Скрываем окно
        }
    }
}