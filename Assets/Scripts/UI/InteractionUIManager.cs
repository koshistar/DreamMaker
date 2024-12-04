using UnityEngine;

public class InteractionUIManager : MonoBehaviour
{
    public GameObject interactionPanel1;  // 面板1

    public InteractionEventManager eventManager;  // 事件管理器引用

    private void Start()
    {
        // 监听交互事件
        eventManager.OnInteractWithObject.AddListener(OnInteractWithObject);
        Debug.Log("Interaction UI Manager initialized.");
    }

    // 交互事件触发后的UI更新
    private void OnInteractWithObject()
    {
        Debug.Log("Interaction event received, showing the appropriate UI panel.");
        ShowPanel(1);  // 打开第一个面板
    }

    // 显示不同的UI面板
    private void ShowPanel(int panelIndex)
    {
        Debug.Log("Showing UI panel " + panelIndex);

        // 隐藏所有面板
        interactionPanel1.SetActive(false);


        // 显示指定的面板
        switch (panelIndex)
        {
            case 1:
                interactionPanel1.SetActive(true);
                Debug.Log("Panel 1 is now active.");
                break;
                Debug.LogWarning("Invalid panel index.");
                break;
        }
    }
}
