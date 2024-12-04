using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    [Header("�ռ���ͳ��")]
    public int collectedItemsCount = 0;  // ͳ������ռ�����Ʒ����

    [Header("UI��ʾ����")]
    public TMPro.TMP_Text collectedItemsText;  // ��ʾ���ռ���Ʒ������UI�ı�����ѡ��

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

    // ����UI��ʾ
    private void UpdateUI()
    {
        if (collectedItemsText != null)
        {
            collectedItemsText.text = "Collected Items: " + collectedItemsCount;
        }
    }
}
