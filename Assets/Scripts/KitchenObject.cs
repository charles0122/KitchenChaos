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

        // 如果物品还在柜台 先清除
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.Log("柜台上已经有物品了");
        }

        kitchenObjectParent.SetKitchenObject(this);

        // 切换柜台时 获取柜台的顶点位置 并改变当前物品所在位置
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetkitchenObjectParent() { 
        return kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetkitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

}