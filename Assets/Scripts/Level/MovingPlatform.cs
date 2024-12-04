using Player;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("�ƶ�����")]
    public Transform pointA;           // ��ʼ��
    public Transform pointB;           // �յ�
    public float moveSpeed = 2f;       
    public float waitTime = 1f;        // ��ÿ����ͣ����ʱ��

    private bool movingToB = true;
    private float timer = 0f; 

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Transform targetPoint = movingToB ? pointB : pointA;

        transform.position = Vector3.Lerp(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                movingToB = !movingToB;
                timer = 0f;
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
                playerController.DisableSizeChange(true);
            }
            collision.transform.SetParent(transform);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // �ָ�����С����
                playerController.DisableSizeChange(false);
            }
            collision.transform.SetParent(null); 
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
