using UnityEngine;
using System.Collections.Generic;

public class PlayerLitController : MonoBehaviour
{
    [Header("��������")]
    public float moveDistance = 110f;  
    private RectTransform rectTransform;  

    [Header("��Χ����")]
    public Vector2 minPosition = new Vector2(-500, -500); 
    public Vector2 maxPosition = new Vector2(500, 500);

    private Vector2 BoxPosition;
    private bool isRecording = false; 
    private bool isCloneActive = false;
    private bool canMove = true;
    private List<Vector2> moveHistory = new List<Vector2>(); 
    private int currentHistoryIndex = 0; 

    [Header("��¡������")]
    public GameObject cloneObject;
    private CloneController cloneController;  
    
    private Vector2 initialPlayerLitPosition;  
    private Vector2[] initialBoxPositions; 

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();


        initialPlayerLitPosition = rectTransform.anchoredPosition;

        if (cloneObject != null)
        {
            cloneObject.SetActive(false);
            cloneController = cloneObject.GetComponent<CloneController>(); 
        }

        StoreInitialBoxPositions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)&&canMove) 
        {
            MovePlayerLit(Vector2.up);
            if(isCloneActive)
                Invoke("PlayCloneHistoryStep",0.5f);
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            MovePlayerLit(Vector2.down);
            if(isCloneActive)
                Invoke("PlayCloneHistoryStep",0.5f);
        }
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            MovePlayerLit(Vector2.left);
            if(isCloneActive)
                Invoke("PlayCloneHistoryStep",0.5f);
        }
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            MovePlayerLit(Vector2.right);
            if(isCloneActive)
                Invoke("PlayCloneHistoryStep",0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecording && !isCloneActive)
            {
                StartRecording();
            }
            else if (isRecording)
            {
                StopRecordingAndActivateClone();
                ResetPositions();
            }
            // else if (isCloneActive)
            // {
            //     PlayCloneHistoryStep();
            // }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPositions();
            moveHistory.Clear();
            currentHistoryIndex = 0;
        }

        if (cloneObject != null)
        {
            BoxPosition = cloneObject.transform.position;
        }
    }
    
    private void StartRecording()
    {
        moveHistory.Clear(); 
        isRecording = true; 
        Debug.Log("Started recording PlayerLit's movement.");
    }

    private void StopRecordingAndActivateClone()
    {
        isRecording = false; 
        isCloneActive = true; 
        currentHistoryIndex = 0;  

        if (cloneObject != null)
        {
            cloneObject.SetActive(true);
        }

        Debug.Log("Stopped recording. Clone is now active.");
    }

    private void MovePlayerLit(Vector2 direction)
    {
        // if (isRecording)
        // {
            Vector2 newPosition = rectTransform.anchoredPosition + direction * moveDistance;
            newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

            rectTransform.anchoredPosition = newPosition;
            
            if (isRecording)
            {
                moveHistory.Add(rectTransform.anchoredPosition);
                Debug.Log("Recorded new position: " + rectTransform.anchoredPosition);
            }
            
            //PushBoxInDirection(direction);
        // }
        // else if (isCloneActive) 
        // {
        //     Debug.LogWarning("Cannot move PlayerLit while clone is active.");
        // }
    }

    private void PushBoxInDirection(Vector2 direction)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(rectTransform.position, rectTransform.rect.size, 0f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Box"))
            {
                RectTransform boxRect = collider.GetComponent<RectTransform>();
                // ���������ƶ��ķ���
                Vector2 boxNewPosition = boxRect.anchoredPosition + direction * moveDistance;

                // ���������ڷ�Χ��
                boxNewPosition.x = Mathf.Clamp(boxNewPosition.x, minPosition.x, maxPosition.x);
                boxNewPosition.y = Mathf.Clamp(boxNewPosition.y, minPosition.y, maxPosition.y);

                // �������ӵ�λ��
                boxRect.anchoredPosition = boxNewPosition;
                Debug.Log("Box moved in direction: " + direction);
            }
        }
    }

    private void PlayCloneHistoryStep()
    {
        if (currentHistoryIndex < moveHistory.Count && moveHistory.Count > 0)
        {
            Vector2 position = moveHistory[currentHistoryIndex]; 
            cloneController.MoveToPosition(position);  
            currentHistoryIndex++; 

            if (currentHistoryIndex > 1)
            {
                Vector2 direction = moveHistory[currentHistoryIndex - 1] - moveHistory[currentHistoryIndex - 2];
                cloneController.PushBoxInDirection(direction);
            }
        }
        else
        {
            Debug.Log("No more history steps to play.");
            isCloneActive = false; 
            cloneObject.SetActive(false);
        }
    }

    private void StoreInitialBoxPositions()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        initialBoxPositions = new Vector2[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            RectTransform boxRect = boxes[i].GetComponent<RectTransform>();
            initialBoxPositions[i] = boxRect.anchoredPosition;
        }
    }

    private void ResetPositions()
    {
        rectTransform.anchoredPosition = initialPlayerLitPosition;

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < boxes.Length; i++)
        {
            RectTransform boxRect = boxes[i].GetComponent<RectTransform>();
            boxRect.anchoredPosition = initialBoxPositions[i];
        }

        //moveHistory.Clear();
        //currentHistoryIndex = 0;

        Debug.Log("PlayerLit and boxes reset to their initial positions.");
    }
}
