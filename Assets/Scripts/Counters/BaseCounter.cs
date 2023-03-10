using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public  class BaseCounter : MonoBehaviour,IKitchenObjectParent {

    // 物品放下音效
    public static event EventHandler OnAnyObjectPlacedHere;
    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null;
    }

    // 柜台生成 厨房物品SO 的位置
    [SerializeField] private Transform counterTopPoint;
    // 厨房物品
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
            
    }
    
    public virtual void InteractAlternate(Player player) {

    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
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