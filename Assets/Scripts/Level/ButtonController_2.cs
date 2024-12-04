using UnityEngine;

public class ButtonController_2 : MonoBehaviour
{
    public GameObject platform;  // ������ƽ̨����
    public float moveUpDistance = 5f; 
    public float moveSpeed = 2f;
    public float resetSpeed = 1f; 

    private bool isInteracting = false;  
    private Vector3 platformStartPosition;  

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
            MovePlatformUp();
        }
        else
        {
            MovePlatformDown();
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

    // ƽ̨�����ƶ�
    private void MovePlatformUp()
    {
        if (platform != null)
        {
            Vector3 targetPosition = platformStartPosition + Vector3.up * moveUpDistance;
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    // ƽ̨�����½���ԭλ
    private void MovePlatformDown()
    {
        if (platform != null && platform.transform.position != platformStartPosition)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, platformStartPosition, resetSpeed * Time.deltaTime);
        }
    }
}
