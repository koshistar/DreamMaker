using UnityEngine;

public class LargeSkill : MonoBehaviour
{
    [Header("�Ŵ�����")]
    public Transform playerRootTransform;  // �������ŵĸ�����
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
