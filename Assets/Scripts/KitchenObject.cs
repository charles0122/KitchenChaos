using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;


    public KitchenObjectSO GetKitchenObjectSO() { 
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter) {

        // �����Ʒ���ڹ�̨ �����
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject()) {
            Debug.Log("��̨���Ѿ�����Ʒ��");
        }

        clearCounter.SetKitchenObject(this);

        // �л���̨ʱ ��ȡ��̨�Ķ���λ�� ���ı䵱ǰ��Ʒ����λ��
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() { 
        return clearCounter;
    }


}