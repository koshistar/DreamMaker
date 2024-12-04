using UnityEngine;

public class Interaction : MonoBehaviour
{
    public InteractionEventManager eventManager;  // �¼�������������
    private bool isInteracting = false;  // �Ƿ��ڽ���״̬

    private void Update()
    {
        // �򻯵��Լ�飬�Ƿ�ʶ�𰴼�
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed, checking for interaction...");
        }

        if (isInteracting && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed. Triggering interaction...");
            eventManager.TriggerInteract();  // ���������¼�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteracting = true;  // �������
            Debug.Log("Player entered interaction zone: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteracting = false;  // ��ֹ����
            Debug.Log("Player exited interaction zone: " + other.name);
        }
    }
}
