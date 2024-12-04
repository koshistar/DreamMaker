using UnityEngine;
using UnityEngine.Events;

public class InteractionEventManager : MonoBehaviour
{
    public UnityEvent OnInteractWithObject;

    private void Start()
    {
        // ȷ���¼��з�������
        if (OnInteractWithObject == null)
        {
            OnInteractWithObject = new UnityEvent();
            Debug.LogWarning("OnInteractWithObject event is not assigned, creating a new one.");
        }
    }

    // ���������¼�
    public void TriggerInteract()
    {
        Debug.Log("Interaction event triggered!");
        OnInteractWithObject.Invoke();  // �����¼�
    }
}
