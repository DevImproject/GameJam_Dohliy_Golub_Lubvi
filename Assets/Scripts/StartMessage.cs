using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI

public class StartMessage : MonoBehaviour
{
    public Text messageText; // UI ����� ��� ����������� ���������
    public string startMessage = "����� ������� ���, �������� ������ ������. ���� ���������� ������ ���� ����� ���������� ����� �����������, ����� ��������� ����� USB-������ � ���� ����������, ��� ������� �� ���!"; // ����� ���������
    public float displayDuration = 5f; // ������������ ����������� ������ � ��������

    private void Start()
    {
        if (messageText != null)
        {
            // ������������� ����� ���������
            messageText.text = startMessage;

            // ��������� �������� ��� ������� ������ ����� 5 ������
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private System.Collections.IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // ���� ��������� ���������� ������

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // �������� �����
        }

        // ���� ������������, ����� �������� �������������� ������ �����
    }
}