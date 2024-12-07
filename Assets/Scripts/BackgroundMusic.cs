using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // Звуковой клип для музыки
    private AudioSource audioSource;   // Компонент AudioSource для воспроизведения музыки

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
    }
}