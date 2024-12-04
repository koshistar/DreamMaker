using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("物理层")]
        public LayerMask interactionLayer;
        private Rigidbody rb;

        [Header("移动")]
        public float moveSpeed = 5f;
        public float sprintSpeed = 10f;           // 冲刺速度
        private Vector2 movementInput;            // 移动输入
        private PlayerInputActions inputActions;   // 输入系统动作

        [Header("跳跃")]
        public float jumpForce = 10f;
        public float groundCheckDistance = 0.3f;
        public LayerMask groundLayer;             // 地面层

        [Header("动画")]
        public float defaultAnimationSpeed = 2f;
        public Animator CharacterAnimator;
        public bool DisableAnimatorLogs = true;

        [Header("调试")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isSprinting;

        [Header("技能")]
        public ShrinkSkill shrinkSkill;
        public LargeSkill largeSkill;

        public SizeState currentSizeState = SizeState.Normal;  // 大小状态
        private bool canChangeSize = true;

        [Header("Player Spine")]
        public Transform playerSpine;  // 玩家脊柱物体，用于旋转

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputActions = new PlayerInputActions();

            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

            rb.drag = 2f;
            rb.angularDrag = 2f;
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Update()
        {
            HandleMovement();
            HandleJump();
            HandleSprint();
            HandleGroundCheck();
            HandleShrinkSkill();
            HandleLargeSkill();
            UpdatePlayerSpineRotation();  // 更新PlayerSpine的旋转
            UpdateAnimations();           // 更新动画状态
        }

        private void HandleMovement()
        {
            // 获取移动输入
            movementInput = inputActions.Player.Move.ReadValue<Vector2>();

            if (movementInput.magnitude >= 0.1f)
            {
                Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
                moveDirection = transform.TransformDirection(moveDirection);

                float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

                Vector3 velocity = moveDirection * currentSpeed * Time.deltaTime;
                velocity.y = rb.velocity.y;
                rb.velocity = velocity;
            }
        }

        private void HandleJump()
        {
            // 检测跳跃
            if (inputActions.Player.Jump.triggered && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void HandleSprint()
        {
            // 检查冲刺
            isSprinting = inputActions.Player.Sprint.IsPressed();
        }

        private void HandleGroundCheck()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        private void HandleLargeSkill()
        {
            // 触发变大技能
            if (canChangeSize && inputActions.Player.Large.triggered)
            {
                if (currentSizeState == SizeState.Large)
                {
                    largeSkill.RestorePlayerSize();
                    currentSizeState = SizeState.Normal;

                    rb.mass = 1f;
                }
                else
                {
                    largeSkill.LargePlayer();
                    currentSizeState = SizeState.Large;

                    rb.mass = 8f;
                }
            }
        }

        private void HandleShrinkSkill()
        {
            // 触发变小技能
            if (canChangeSize && inputActions.Player.Shrink.triggered)
            {
                if (currentSizeState == SizeState.Shrunk)
                {
                    shrinkSkill.RestorePlayerSize();
                    currentSizeState = SizeState.Normal;

                    rb.mass = 1f;
                }
                else
                {
                    shrinkSkill.ShrinkPlayer();
                    currentSizeState = SizeState.Shrunk;

                    rb.mass = 1f;
                }
            }
        }

        public void DisableSizeChange(bool disable)
        {
            canChangeSize = !disable;
        }

        private void UpdatePlayerSpineRotation()
        {
            Vector3 velocityDirection = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized;

            if (movementInput.magnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocityDirection);

                playerSpine.rotation = Quaternion.Slerp(playerSpine.rotation, targetRotation, Time.deltaTime * 10f);
            }
            else
            {
                float currentYRotation = playerSpine.rotation.eulerAngles.y;
                playerSpine.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
            }
        }

        private void UpdateAnimations()
        {
            // 判断是否在地面上
            if (isGrounded)
            {
                // 停止跳跃动画
                CharacterAnimator.SetBool("isJumping", false);

                // 判断是否在移动
                if (movementInput.magnitude >= 0.1f)
                {
                    if (isSprinting)
                    {
                        // 播放奔跑动画
                        CharacterAnimator.SetBool("isRunning", true);
                        CharacterAnimator.SetBool("isWalking", false);
                        CharacterAnimator.SetBool("isIdle", false);
                    }
                    else
                    {
                        // 播放走路动画
                        CharacterAnimator.SetBool("isWalking", true);
                        CharacterAnimator.SetBool("isRunning", false);
                        CharacterAnimator.SetBool("isIdle", false);
                    }
                }
                else
                {
                    // 停止走路/跑步动画并播放Idle动画
                    CharacterAnimator.SetBool("isWalking", false);
                    CharacterAnimator.SetBool("isRunning", false);
                    CharacterAnimator.SetBool("isIdle", true);
                }
            }
            else
            {
                // 播放跳跃动画
                CharacterAnimator.SetBool("isJumping", true);
                CharacterAnimator.SetBool("isIdle", false);
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector3.down * groundCheckDistance);
        }
    }

    public enum SizeState
    {
        Normal,
        Shrunk,
        Large
    }
}
