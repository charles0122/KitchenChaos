using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    // 有效的原料表 可以放在盘子上
    [SerializeField] private List<KitchenObjectSO> vaildKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

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
            return true;
        }
    }
}