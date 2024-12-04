using UnityEngine;

public class ButtonController_3 : MonoBehaviour
{
    public GameObject platform;  // 关联的平台对象
    public float moveBackwardDistance = 5f; 
    public float moveSpeed = 2f;  // 移动速度
    public float resetSpeed = 1f;  // 还原速度

    private bool isInteracting = false;
    private Vector3 platformStartPosition;  // 平台初始位置

    private void Start()
    {
        if (platform != null)
        {
            platformStartPosition = platform.transform.position;
        }
    }

    private void Update()
    {
        if (isInteracting)
        {
            MovePlatformBackward();
        }
        else
        {
            MovePlatformBackToStart();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Button triggered by Player!");
            isInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteracting = false;
        }
    }

    private void MovePlatformBackward()
    {
        if (platform != null)
        {
            Vector3 targetPosition = platformStartPosition + Vector3.back * moveBackwardDistance; 
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void MovePlatformBackToStart()
    {
        if (platform != null && platform.transform.position != platformStartPosition)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, platformStartPosition, resetSpeed * Time.deltaTime);
        }
    }
}
