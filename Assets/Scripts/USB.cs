using UnityEngine;
using UnityEngine.UI; // ��� ������ ��������� � UI

public class USB : MonoBehaviour
{
    public Text messageText; // ������ �� UI �����, ���� ����� ���������� ���������
    public Image messageBG; // ������ �� UI �����, ���� ����� ���������� ���������
    private bool isStuck = false; // ����, ����� �����������, �������� �� �����
    public float displayDuration = 3f; // ������������ ����������� ������ � ��������

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
                messageBG.gameObject.SetActive(true); // �������� �����
                //ShowMessage("��� ������, USB-������ ������� �� ��� ��������... ����� ����������� ��� � �������� �����!"); // ���������� ��������� � ������������ ����������
                StopBird(collision); // ������������� �������� �����
          
            }
            else
            {
                //ShowMessage(""); // ������� ���������, ���� ����� ��������� �����������
                isStuck = false; // ���������� ���� �����������
                StartCoroutine(HideMessageAfterDelay());
            }
        }
        else
        {
            StartCoroutine(HideMessageAfterDelay());
            //ShowMessage(""); // ������� ���������, ���� ����� ��������� �����������
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

    private System.Collections.IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // ���� ��������� ���������� ������
        //ShowMessage(""); // ������� ���������, ���� ����� ��������� �����������
        messageBG.gameObject.SetActive(false); // �������� �����
    }
}