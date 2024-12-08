using UnityEngine;
using UnityEngine.UI;

public class AuthorsWindow : MonoBehaviour
{
    public Image authorsPanel; // ������, ��� ��������� ���� �������
    public Button toggleButton;
    private bool isMuted = false; // ���� ��� �������� ��������� �����

    void Start()
    {
        // ���������, ��� ���� ������� ������ � ������
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleAvtor);
        }
    }

    private void ToggleAvtor()
    {
        isMuted = !isMuted; // ������ ��������� �����

        if (isMuted)
        {
            authorsPanel.gameObject.SetActive(true);
        }
        else
        {
            authorsPanel.gameObject.SetActive(false); // ������������ ���������������
        }
    }

    // ����� ��� ������ ���� �������
    public void ShowAuthors()
    {
        if (authorsPanel != null)
        {
            authorsPanel.gameObject.SetActive(true); // ���������� ����
        }
    }

    // ����� ��� ������� ���� �������
    public void HideAuthors()
    {
        if (authorsPanel != null)
        {
            authorsPanel.gameObject.SetActive(false); // �������� ����
        }
    }
}