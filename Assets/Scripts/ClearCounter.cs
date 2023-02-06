using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour {
    // ������ƷSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // ��̨���� ������ƷSO ��λ��
    [SerializeField] private Transform counterTopPoint;
    // ������Ʒ
    private KitchenObject kitchenObject;

    public void Interact() {
        if (kitchenObject==null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        } else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
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
 