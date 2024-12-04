using UnityEngine;

public class LargeSkill : MonoBehaviour
{
    [Header("放大设置")]
    public Transform playerRootTransform;  // 用于缩放的父物体
    public float Factor = 2f;
    private Vector3 originalScale;

    private void Start()
    {
        if (playerRootTransform != null)
        {
            originalScale = playerRootTransform.localScale;
        }
    }

    public void LargePlayer()
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
