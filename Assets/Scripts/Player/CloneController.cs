using UnityEngine;

public class CloneController : MonoBehaviour
{
    private RectTransform rectTransform; 

    [Header("��Χ����")]
    public Vector2 minPosition = new Vector2(-500, -500); 
    public Vector2 maxPosition = new Vector2(500, 500);  

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); 
    }

    public void MoveToPosition(Vector2 position)
    {
        position.x = Mathf.Clamp(position.x, minPosition.x, maxPosition.x);
        position.y = Mathf.Clamp(position.y, minPosition.y, maxPosition.y);

        rectTransform.anchoredPosition = position;
    }

    public void PushBoxInDirection(Vector2 direction)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(rectTransform.position, rectTransform.rect.size, 0f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Box"))
            {
                RectTransform boxRect = collider.GetComponent<RectTransform>();
                // ���������ƶ��ķ���
                Vector2 boxNewPosition = boxRect.anchoredPosition + direction; 

                // ���������ڷ�Χ��
                boxNewPosition.x = Mathf.Clamp(boxNewPosition.x, minPosition.x, maxPosition.x);
                boxNewPosition.y = Mathf.Clamp(boxNewPosition.y, minPosition.y, maxPosition.y);

                // �������ӵ�λ��
                boxRect.anchoredPosition = boxNewPosition;
                Debug.Log("Box moved in direction: " + direction);
            }
        }
    }
}
