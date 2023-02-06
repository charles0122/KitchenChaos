using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour,IKitchenObjectParent {

    // 厨房物品SO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // 柜台生成 厨房物品SO 的位置
    [SerializeField] private Transform counterTopPoint;
    // 厨房物品
    private KitchenObject kitchenObject;

    public void  Interact(Player player) {
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(this);
        } else {
            Debug.Log(kitchenObject.GetkitchenObjectParent());
            kitchenObject.SetkitchenObjectParent(player);
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