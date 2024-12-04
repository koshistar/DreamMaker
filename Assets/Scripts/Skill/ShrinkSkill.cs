using UnityEngine;

public class ShrinkSkill : MonoBehaviour
{
    [Header("��С����")]
    public Transform playerRootTransform;  // ��ɫ������
    public float Factor = 0.5f;            // ��С����
    private Vector3 originalScale;         // ԭʼ����ֵ

    private void Start()
    {
        if (playerRootTransform != null)
        {
            originalScale = playerRootTransform.localScale; 
        }
    }

    // ������С����
    public void ShrinkPlayer()
    {
        if (playerRootTransform != null)
        {
            playerRootTransform.localScale = originalScale * Factor; 
        }
    }
    public void RestorePlayerSize()
    {
        if (playerRootTransform != null)
        {
            playerRootTransform.localScale = originalScale; 
        }
    }
}
