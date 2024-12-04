using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class GhostClone : MonoBehaviour
    {
        [Header("幽灵状态设置")]
        public float ghostTime = 3f;  // 幽灵状态持续时间
        private bool isGhosting = false;
        private bool isReplaying = false; 
        private float ghostTimer = 0f;

        [Header("玩家设置")]
        private Rigidbody rb;
        private PlayerInputActions inputActions;
        public Transform playerSpine;
        public Transform playerRoot;

        private Vector3 startPosition;
        private Quaternion startRotation;

        private List<PlayerAction> recordedActions = new List<PlayerAction>();
        private bool isRecording = false;

        private GameObject clone;
        public Vector3 cloneScale = new Vector3(1f, 1f, 1f);

        [Header("UI设置")]
        public Canvas ghostCanvas;
        public GameObject ghostImage;

        private void Awake()
        {
            rb = playerRoot.GetComponent<Rigidbody>();
            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Start()
        {
            ghostCanvas.enabled = false;
            ghostImage.SetActive(false);
        }

        private void Update()
        {
            if (isGhosting)
            {
                ghostTimer += Time.deltaTime;

                if (inputActions.Player.GhostMode.triggered || ghostTimer >= ghostTime)
                {
                    EndGhostMode();
                }

                if (isRecording)
                {
                    RecordPlayerAction();
                }
            }
            else
            {
                if (inputActions.Player.GhostMode.triggered && !isReplaying)
                {
                    StartGhostMode();
                }
            }
        }

        private void StartGhostMode()
        {
            recordedActions.Clear();

            isGhosting = true;
            ghostTimer = 0f;
            startPosition = playerRoot.position;
            startRotation = playerRoot.rotation;


            ghostCanvas.enabled = true;
            ghostImage.SetActive(true);

            isRecording = true;
        }

        private void EndGhostMode()
        {
            isGhosting = false;
            isRecording = false;

            ClonePlayer();

            playerRoot.position = startPosition;
            playerRoot.rotation = startRotation;

            ghostCanvas.enabled = false;
            ghostImage.SetActive(false);

            StartCoroutine(ReplayRecordedActions());
        }

        private void RecordPlayerAction()
        {
            Vector3 playerPosition = playerRoot.position;
            Quaternion playerRotation = playerRoot.rotation;

            recordedActions.Add(new PlayerAction(playerPosition, playerRotation));
        }

        private IEnumerator ReplayRecordedActions()
        {
            isReplaying = true;

            foreach (var action in recordedActions)
            {
                clone.transform.position = action.position;
                clone.transform.rotation = action.rotation;

                yield return new WaitForSeconds(0.0001f);
            }

            yield return new WaitForSeconds(3f);
            Destroy(clone);

            isReplaying = false;
        }

        private void ClonePlayer()
        {
            clone = Instantiate(playerRoot.gameObject, playerRoot.position, playerRoot.rotation);
            clone.SetActive(true);

            clone.tag = "Player";

            RemoveControlComponents(clone);

            Collider cloneCollider = clone.GetComponent<Collider>();
            if (cloneCollider != null)
            {
                cloneCollider.isTrigger = true; 
            }

            SetTagForAllChildren(clone.transform);

            clone.transform.localScale = cloneScale;
        }

        private void RemoveControlComponents(GameObject clone)
        {

            PlayerController controller = clone.GetComponent<PlayerController>();
            if (controller != null)
            {
                Destroy(controller);
            }

            PlayerInput input = clone.GetComponent<PlayerInput>();
            if (input != null)
            {
                Destroy(input);
            }

        }

        private void SetTagForAllChildren(Transform parent)
        {
            // 遍历所有子物体
            foreach (Transform child in parent)
            {
                child.tag = "Player"; 

                if (child.childCount > 0)
                {
                    SetTagForAllChildren(child);
                }
            }
        }

        public struct PlayerAction
        {
            public Vector3 position;
            public Quaternion rotation;

            public PlayerAction(Vector3 position, Quaternion rotation)
            {
                this.position = position;
                this.rotation = rotation;
            }
        }
    }
}
