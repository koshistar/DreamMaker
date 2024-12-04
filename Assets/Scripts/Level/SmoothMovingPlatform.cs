using Player;
using UnityEngine;

public class SmoothReturnPlatform : MonoBehaviour
{
    [Header("移动设置")]
    public Transform pointA;           // 起始点
    public Transform pointB;           // 终点
    public float moveSpeed = 2f;

    private float journeyLength;
    private float startTime;
    private bool movingToB = true;

    private void Start()
    {
        journeyLength = Vector3.Distance(pointA.position, pointB.position);
        startTime = Time.time;
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        if (movingToB)
        {
            transform.position = Vector3.Lerp(pointA.position, pointB.position, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                movingToB = false;
                startTime = Time.time;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(pointB.position, pointA.position, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                movingToB = true;
                startTime = Time.time;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // 禁用变大变小技能
                playerController.DisableSizeChange(true);
            }

            collision.transform.SetParent(transform);  // 将玩家与平台绑定
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // 恢复变大变小技能
                playerController.DisableSizeChange(false);
            }

            collision.transform.SetParent(null);  // 玩家与平台解绑
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
    }
}
