using UnityEngine;
using UnityEngine.Events;

public class InteractionEventManager : MonoBehaviour
{
    public UnityEvent OnInteractWithObject;

    private void Start()
    {
        // 确保事件有方法监听
        if (OnInteractWithObject == null)
        {
            OnInteractWithObject = new UnityEvent();
            Debug.LogWarning("OnInteractWithObject event is not assigned, creating a new one.");
        }
    }

    // 触发交互事件
    public void TriggerInteract()
    {
        Debug.Log("Interaction event triggered!");
        OnInteractWithObject.Invoke();  // 触发事件
    }
}
