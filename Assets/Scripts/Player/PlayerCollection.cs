using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    [Header("收集物统计")]
    public int collectedItemsCount = 0;  // 统计玩家收集的物品数量

    [Header("UI显示设置")]
    public TMPro.TMP_Text collectedItemsText;  // 显示已收集物品数量的UI文本（可选）

    private void Start()
    {
        if (collectedItemsText != null)
        {
            UpdateUI();
        }
    }

    public void CollectItem()
    {
        collectedItemsCount++;
        UpdateUI();
    }

    // 更新UI显示
    private void UpdateUI()
    {
        if (collectedItemsText != null)
        {
            collectedItemsText.text = "Collected Items: " + collectedItemsCount;
        }
    }
}
