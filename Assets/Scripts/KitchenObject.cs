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

        // 如果物品还在柜台 先清除
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject()) {
            Debug.Log("柜台上已经有物品了");
        }

        clearCounter.SetKitchenObject(this);

        // 切换柜台时 获取柜台的顶点位置 并改变当前物品所在位置
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() { 
        return clearCounter;
    }


}