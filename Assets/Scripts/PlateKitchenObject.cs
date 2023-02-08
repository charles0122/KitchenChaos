using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject {

    // 有效的原料表 可以放在盘子上
    [SerializeField] private List<KitchenObjectSO> vaildKitchenObjectSOList;
    // 当前盘子上的原料
    private List<KitchenObjectSO> kitchenObjectSOList;


    // 原料添加事件
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!vaildKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // 当前原料不是有效原料
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // 当前原料列表已经有该原料了
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }
}