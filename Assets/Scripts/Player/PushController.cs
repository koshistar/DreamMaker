using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PushController : MonoBehaviour
{
    public GameObject box;
    public float moveDistance = 3f;
    public int movePoint;
    
    public Text pointText;

    private int mp;
    private bool isRecording = false; 
    private bool isCloneActive = false;
    private bool canMove = false;
    
    private List<Vector3> moveHistory = new List<Vector3>(); 
    private int currentHistoryIndex = 0;
    public GameObject cloneObject;
    public GameObject flag;
    
    private Vector3 originalPosition;
    private Vector3 originalPlayerPosition;
    
    [Header("开爆内存吧")]
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> bridges = new List<GameObject>();
    public List<GameObject> teleportersFrom = new List<GameObject>();
    public List<GameObject> teleportersTo = new List<GameObject>();

    public string NextLevel;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = box.transform.position;
        originalPlayerPosition = transform.position;
        mp = movePoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)&&canMove) 
        {
            if (isCloneActive)
            {
                PlayCloneHistoryStep();
                StartCoroutine(Delayed(Vector2.up));
            }
            else
            {
                MovePlayerLit(Vector2.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.S)&&canMove) 
        {
            if (isCloneActive)
            {
                PlayCloneHistoryStep();
                StartCoroutine(Delayed(Vector2.down));
            }
            else
            {
                MovePlayerLit(Vector2.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.A)&&canMove) 
        {
            if (isCloneActive)
            {
                PlayCloneHistoryStep();
                StartCoroutine(Delayed(Vector2.left));
            }
            else
            {
                MovePlayerLit(Vector2.left);
            }
        }
        if (Input.GetKeyDown(KeyCode.D)&&canMove) 
        {
            if (isCloneActive)
            {
                PlayCloneHistoryStep();
                StartCoroutine(Delayed(Vector2.right));
            }
            else
            {
                MovePlayerLit(Vector2.right);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecording && !isCloneActive)
            {
                canMove = true;
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
            cloneObject.SetActive(false);
            mp = movePoint;
            canMove = true;
        }

        // if (cloneObject != null)
        // {
        //     BoxPosition = cloneObject.transform.position;
        // }

        pointText.text = mp.ToString() + " 行动点";
    }
    //延时调用
    IEnumerator Delayed(Vector2 direction)
    {
        yield return new WaitForSeconds(0.5f);
        MovePlayerLit(direction);
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
        canMove = true;
        mp = movePoint;

        cloneObject.SetActive(true);

        Debug.Log("Stopped recording. Clone is now active.");
    }

    private void MovePlayerLit(Vector2 direction)
    {
        --mp;
        if (mp == 0)
            canMove = false;
        Vector3 newPosition = new Vector3(transform.position.x + direction.x * moveDistance,
            transform.position.y + direction.y * moveDistance, 2);
        bool ismoved = true;
        
        foreach (var wall in walls)
        {
            if (wall.transform.position == newPosition)
            {
                ismoved = false;
            }
        }
        
        foreach (var bridge in bridges)
        {
            if (bridge.transform.position == newPosition)
            {
                ismoved = false;
            }
        }
        
        if (ismoved)
        {   
            if (newPosition == box.transform.position)
            {
                if (PushBoxInDirection(direction))
                {
                    transform.position = newPosition;
                }
            }
            else
            {
                transform.position = newPosition;
            }
        }

        //放下吊桥
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].transform.position == transform.position)
            {
                bridges[i].transform.position += Vector3.forward * 4;
                // buttons.Remove(buttons[i]);
                // bridges.Remove(bridges[i]);
            }
        }
        //进入传送门
        for (int i = 0; i < teleportersFrom.Count; i++)
        {
            if (teleportersFrom[i].transform.position == transform.position &&
                box.transform.position != teleportersTo[i].transform.position) 
            {
                transform.position = teleportersTo[i].transform.position;
            }
        }
        if (isRecording)
        {
            moveHistory.Add(transform.position);
            Debug.Log("Recorded new position: " + transform.position);
        }
    }

    //返回bool值判断是否可推
    private bool PushBoxInDirection(Vector2 direction)
    {
        Vector3 newPosition = new Vector3(box.transform.position.x + direction.x * moveDistance,
            box.transform.position.y + direction.y * moveDistance, 2);
        foreach (var wall in walls)
        {
            if (wall.transform.position == newPosition)
            {
                return false;
            }
        }
        foreach (var bridge in bridges)
        {
            if (bridge.transform.position == newPosition)
            {
                return false;
            }
        }

        if (newPosition == transform.position || newPosition == cloneObject.transform.position)
            return false;
        box.transform.position = newPosition;
        AudioManaager.Instance.Play("box");
        //放下吊桥
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].transform.position == box.transform.position)
            {
                bridges[i].transform.position += Vector3.forward * 4;
                // buttons.Remove(buttons[i]);
                // bridges.Remove(bridges[i]);
            }
        }
        //进入传送门
        for (int i = 0; i < teleportersFrom.Count; i++)
        {
            if (teleportersFrom[i].transform.position == box.transform.position &&
                teleportersTo[i].transform.position != transform.position &&
                teleportersTo[i].transform.position != cloneObject.transform.position) 
            {
                box.transform.position = teleportersTo[i].transform.position;
            }
        }
        
        if (box.transform.position == flag.transform.position)
            Win();
        return true;
    }
    private void PlayCloneHistoryStep()
    {
        if (currentHistoryIndex < moveHistory.Count && moveHistory.Count > 0)
        {
            Vector3 position = moveHistory[currentHistoryIndex];
            if(currentHistoryIndex<=1)
                cloneObject.transform.position = position;
            // cloneController.MoveToPosition(position);  
            currentHistoryIndex++; 

            if (currentHistoryIndex > 1)
            {
                Vector2 direction = moveHistory[currentHistoryIndex - 1] - moveHistory[currentHistoryIndex - 2];
                Debug.Log(direction);
                if (position == box.transform.position)
                {
                    if (PushBoxInDirection(direction/3))
                    {
                        cloneObject.transform.position = position;
                    }
                }
                else
                {
                    cloneObject.transform.position = position;
                }
                // cloneController.PushBoxInDirection(direction);
            }
            //放下吊桥
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].transform.position == cloneObject.transform.position)
                {
                    bridges[i].transform.position += Vector3.forward * 4;
                    // buttons.Remove(buttons[i]);
                    // bridges.Remove(bridges[i]);
                }
            }
            //进入传送门
            for (int i = 0; i < teleportersFrom.Count; i++)
            {
                if (teleportersFrom[i].transform.position == cloneObject.transform.position &&
                    box.transform.position != teleportersTo[i].transform.position) 
                {
                    cloneObject.transform.position = teleportersTo[i].transform.position;
                }
            }
        }
        else
        {
            Debug.Log("No more history steps to play.");
            isCloneActive = false; 
            cloneObject.SetActive(false);
        }
    }
    private void ResetPositions()
    {
        transform.position = originalPlayerPosition;
        box.transform.position = originalPosition;
        cloneObject.transform.position = originalPlayerPosition;
        foreach (var bridge in bridges)
        {
            bridge.transform.position -= Vector3.forward * 4;
        }
        //cloneObject.SetActive(false);
        // rectTransform.anchoredPosition = initialPlayerLitPosition;

        // GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        // for (int i = 0; i < boxes.Length; i++)
        // {
        //     RectTransform boxRect = boxes[i].GetComponent<RectTransform>();
        //     boxRect.anchoredPosition = initialBoxPositions[i];
        // }

        //moveHistory.Clear();
        //currentHistoryIndex = 0;

        Debug.Log("PlayerLit and boxes reset to their initial positions.");
    }

    private void Win()
    {
        walls.Clear();
        buttons.Clear();
        bridges.Clear();
        teleportersFrom.Clear();
        teleportersTo.Clear();
        SceneManager.LoadScene(NextLevel);
    }
}
