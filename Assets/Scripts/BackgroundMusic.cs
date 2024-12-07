using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // �������� ���� ��� ������
    private AudioSource audioSource;   // ��������� AudioSource ��� ��������������� ������

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
    }
}