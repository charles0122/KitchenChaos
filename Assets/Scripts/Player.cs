using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

   
    public static Player Instance { get; private set; }


    // �Ƿ�������
    private bool isWalking;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    // ��󽻻�����
    private Vector3 lastInteractDir;
    // counter ͼ������
    [SerializeField] private LayerMask countersLayerMask;
    // ѡ��counter
    public ClearCounter selectCounter;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("�����ж��Playerʵ��");
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
    /// ��Ʒ����
    /// </summary>
    private void HandleInteractions() {
        // ��ȡ��һ�����ƶ�������������
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // ���ƶ�����xoy��Ϊ xoz���ϵ� ����
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        // (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance)
        // ���ƶ�����Ϊ0ʱ moveDirʱ0���� ���߼��������
        // ��¼��󽻻��ķ��� ��ָ�������Ĳ� countersLayerMask
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            // INFO ? TryGetComponent ����ʡ�Է���Լ�� �����out�����ƶ�
            if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter)) {
                // ��ClearCounter���
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
    /// ����ƶ�
    /// </summary>
    private void HandleMovement() {
        // ��ȡ��һ�����ƶ�������������
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // ���ƶ�����xoy��Ϊ xoz���ϵ� ����
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // �ƶ�����
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // ����X��������ƶ�
            Vector3 moveDirX = new Vector3(inputVector.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                moveDir = moveDirX;
            } else {
                // X�������ƶ�

                // ����Z��������ƶ�
                Vector3 moveDirZ = new Vector3(0f, 0f, inputVector.y).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove) {
                    moveDir = moveDirZ;
                } else {
                    // �κη��򶼲����ƶ�
                }
            }
        }
        if (canMove) {
            // �ƶ� �ı� Player λ��
            transform.position += moveDir * moveDistance;
        }


        isWalking = moveDir != Vector3.zero;
        float rorateSpeed = 10f;
        // ת�� �����ƶ����� ʹ�����Բ�ֵ��ʽ
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rorateSpeed * Time.deltaTime);
    }


}