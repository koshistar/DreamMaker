using UnityEngine;

public class ShrinkSkill : MonoBehaviour
{
    [Header("缩小设置")]
    public Transform playerRootTransform;  // 角色根物体
    public float Factor = 0.5f;            // 缩小比例
    private Vector3 originalScale;         // 原始缩放值

    private void Start()
    {
        if (playerRootTransform != null)
        {
            originalScale = playerRootTransform.localScale; 
        }
    }

    // 启动缩小技能
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
