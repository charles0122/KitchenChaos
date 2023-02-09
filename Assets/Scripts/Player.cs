using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IKitchenObjectParent {

   
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
    public BaseCounter selectCounter;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    // ������Ʒ
    private KitchenObject kitchenObject;
    // ��̨���� ������ƷSO ��λ��
    [SerializeField] private Transform kitchenObjectHoldParent;

    public event EventHandler OnPickedSomething;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("�����ж��Playerʵ��");
        }
        Instance = this;
    }
    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) {
            return;
        }
        selectCounter?.InteractAlternate(this);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) {
            return;
        }
        //Debug.Log(selectCounter);
        selectCounter?.Interact(this);
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
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter)) {
                // ��ClearCounter���
                // clearCounter.Interact();
                if (baseCounter != selectCounter) {
                    Debug.Log(baseCounter);
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }


        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
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
        bool canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // ����X��������ƶ�
            Vector3 moveDirX = new Vector3(inputVector.x, 0f, 0f).normalized;
            canMove = (moveDir.x <-.5f || moveDir.x>+.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                moveDir = moveDirX;
            } else {
                // X�������ƶ�

                // ����Z��������ƶ�
                Vector3 moveDirZ = new Vector3(0f, 0f, inputVector.y).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
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

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldParent;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        // ���ż�����Ч
        if (kitchenObject!=null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return (kitchenObject != null);
    }
}