using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController doorController;  // �������ſ�����
    private bool isInteracting = false;  // ����Ƿ��밴ť�Ӵ�

    private void Update()
    {
        if (isInteracting)
        {
            doorController.OpenDoor(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Button triggered by Player!");
            AudioManaager.Instance.Play("clickpop");  
            isInteracting = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteracting = false;  
        }
    }
}
