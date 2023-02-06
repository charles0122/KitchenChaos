using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

   
    public static Player Instance { get; private set; }


    // 是否在行走
    private bool isWalking;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    // 最后交互方向
    private Vector3 lastInteractDir;
    // counter 图层遮罩
    [SerializeField] private LayerMask countersLayerMask;
    // 选中counter
    public ClearCounter selectCounter;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("这里有多个Player实例");
        }
        Instance = this;
    }
    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        //Debug.Log(selectCounter);
        selectCounter?.Interact();
    }

    private void Update(){
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    /// <summary>
    /// 物品交互
    /// </summary>
    private void HandleInteractions() {
        // 获取归一化的移动方向输入向量
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // 将移动方向xoy改为 xoz面上的 方向
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        // (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance)
        // 当移动向量为0时 moveDir时0向量 射线检测无作用
        // 记录最后交互的方向 并指定交互的层 countersLayerMask
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            // INFO ? TryGetComponent 可以省略泛型约束 会根据out参数推断
            if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter)) {
                // 有ClearCounter组件
                // clearCounter.Interact();
                if (clearCounter != selectCounter) {
                    SetSelectedCounter(clearCounter);

                }
            } else {
                SetSelectedCounter(null);
            }


        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    private void HandleMovement() {
        // 获取归一化的移动方向输入向量
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // 将移动方向xoy改为 xoz面上的 方向
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // 移动距离
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // 假设X方向可以移动
            Vector3 moveDirX = new Vector3(inputVector.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                moveDir = moveDirX;
            } else {
                // X方向不能移动

                // 假设Z方向可以移动
                Vector3 moveDirZ = new Vector3(0f, 0f, inputVector.y).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove) {
                    moveDir = moveDirZ;
                } else {
                    // 任何方向都不能移动
                }
            }
        }
        if (canMove) {
            // 移动 改变 Player 位置
            transform.position += moveDir * moveDistance;
        }


        isWalking = moveDir != Vector3.zero;
        float rorateSpeed = 10f;
        // 转向 面向移动方向 使用线性插值方式
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rorateSpeed * Time.deltaTime);
    }


}