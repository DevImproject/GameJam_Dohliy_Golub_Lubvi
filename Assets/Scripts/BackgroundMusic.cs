using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // Звуковой клип для музыки
    private AudioSource audioSource;   // Компонент AudioSource для воспроизведения музыки

    public Button toggleButton; // Кнопка для управления звуком
    public Sprite soundOnSprite; // Изображение кнопки при включенном звуке
    public Sprite soundOffSprite; // Изображение кнопки при выключенном звуке

    private bool isMuted = false; // Флаг для проверки состояния звука

    void Start()
    {
        // Добавляем компонент AudioSource, если его еще нет
        audioSource = gameObject.AddComponent<AudioSource>();

        // Устанавливаем музыкальный клип
        audioSource.clip = backgroundMusic;

        // Настройка зацикливания музыки
        audioSource.loop = true;

        audioSource.volume = 0.1f;

        // Включаем воспроизведение
        audioSource.Play();

        // Привязываем метод ToggleSound к кнопке
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleSound);
            UpdateButtonImage(); // Устанавливаем начальное изображение кнопки
        }
    }

    // Метод для переключения звука
    private void ToggleSound()
    {
        isMuted = !isMuted; // Меняем состояние звука

        if (isMuted)
        {
            audioSource.Pause(); // Приостанавливаем звук
        }
        else
        {
            audioSource.Play(); // Возобновляем воспроизведение
        }

        UpdateButtonImage(); // Обновляем изображение кнопки
    }

    // Метод для обновления изображения кнопки
    private void UpdateButtonImage()
    {
        if (toggleButton != null)
        {
            Image buttonImage = toggleButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
            }
        }
    }
}