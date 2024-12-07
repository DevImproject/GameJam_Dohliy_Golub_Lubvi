using UnityEngine;
using UnityEngine.UI; // ��� ������ ��������� � UI

public class USB : MonoBehaviour
{
    public Text messageText; // ������ �� UI �����, ���� ����� ���������� ���������
    private bool isStuck = false; // ����, ����� �����������, �������� �� �����

    void OnCollisionEnter(Collision collision)
    {
        // ���������, ����������� �� ����� � �������� USB
        if (collision.collider.CompareTag("Bird"))
        {
            // �������� ���� �������� ����� ������������ USB
            float angle = Vector3.Angle(collision.transform.up, collision.contacts[0].normal);

            // ���� ���� ������ 45 ��������, ����� "��������" � USB, �� ��� ��������
            if (angle > 45f & !isStuck)
            {
                isStuck = true; // ������������� ���� �����������
                ShowMessage("�� ��� �������� ���������"); // ���������� ��������� � ������������ ����������
                StopBird(collision); // ������������� �������� �����
            }
            else
            {
                ShowMessage(""); // ������� ���������, ���� ����� ��������� �����������
                isStuck = false; // ���������� ���� �����������
            }
        }
        else
        {
            ShowMessage(""); // ������� ���������, ���� ����� ��������� �����������
        }
    }

    // ����� ��� ��������� �������� ����� ��� �����������
    private void StopBird(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // ������������� ������, ����� ��������
            rb.velocity = Vector3.zero; // �������� ��������, ����� ����� �� ���������
        }
    }

    // ����� ��� ����������� ��������� �� UI
    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message; // ���������� ���������� ���������
        }
    }
}