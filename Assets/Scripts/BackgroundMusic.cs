using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // �������� ���� ��� ������
    private AudioSource audioSource;   // ��������� AudioSource ��� ��������������� ������

    public Button toggleButton; // ������ ��� ���������� ������
    public Sprite soundOnSprite; // ����������� ������ ��� ���������� �����
    public Sprite soundOffSprite; // ����������� ������ ��� ����������� �����

    private bool isMuted = false; // ���� ��� �������� ��������� �����

    void Start()
    {
        // ��������� ��������� AudioSource, ���� ��� ��� ���
        audioSource = gameObject.AddComponent<AudioSource>();

        // ������������� ����������� ����
        audioSource.clip = backgroundMusic;

        // ��������� ������������ ������
        audioSource.loop = true;

        audioSource.volume = 0.1f;

        // �������� ���������������
        audioSource.Play();

        // ����������� ����� ToggleSound � ������
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleSound);
            UpdateButtonImage(); // ������������� ��������� ����������� ������
        }
    }

    // ����� ��� ������������ �����
    private void ToggleSound()
    {
        isMuted = !isMuted; // ������ ��������� �����

        if (isMuted)
        {
            audioSource.Pause(); // ���������������� ����
        }
        else
        {
            audioSource.Play(); // ������������ ���������������
        }

        UpdateButtonImage(); // ��������� ����������� ������
    }

    // ����� ��� ���������� ����������� ������
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