using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter :BaseCounter {
    // 厨房物品SO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
        } else {
            // 玩家手上有东西
            if (player.HasKitchenObject()) {
                // 如果 玩家手上的物品是 PlateKitchenObject 类型的
                if (player.GetKitchenObject() is PlateKitchenObject) {
                    // 手持盘子
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    // 尝试将桌面上的物品放到盘子中
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                    
                }
            } else {
                GetKitchenObject().SetkitchenObjectParent(player);
            }
        }
    }

    
}
 