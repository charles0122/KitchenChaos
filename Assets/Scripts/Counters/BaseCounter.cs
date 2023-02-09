using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public  class BaseCounter : MonoBehaviour,IKitchenObjectParent {

    // ��Ʒ������Ч
    public static event EventHandler OnAnyObjectPlacedHere;
    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null;
    }

    // ��̨���� ������ƷSO ��λ��
    [SerializeField] private Transform counterTopPoint;
    // ������Ʒ
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