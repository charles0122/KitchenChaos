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
                // 如果玩家手中有物品
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // 尝试获取手持盘子 如果没有手持盘子 plateKitchenObject = null
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // 尝试将桌面上的物品放到盘子中

                        // 销毁柜台中的物品
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // 玩家手持除盘子以外的物品
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // 柜台上放着盘子
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            // 尝试往盘子中添加原料

                            // 销毁玩家手中的物品
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                GetKitchenObject().SetkitchenObjectParent(player);
            }
        }
    }

    
}
 