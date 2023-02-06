using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;


    public KitchenObjectSO GetKitchenObjectSO() { 
        return kitchenObjectSO;
    }

    public void SetkitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {

        // �����Ʒ���ڹ�̨ �����
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.Log("��̨���Ѿ�����Ʒ��");
        }

        kitchenObjectParent.SetKitchenObject(this);

        // �л���̨ʱ ��ȡ��̨�Ķ���λ�� ���ı䵱ǰ��Ʒ����λ��
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetkitchenObjectParent() { 
        return kitchenObjectParent;
    }

}