using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter :BaseCounter {
    // ³ø·¿ÎïÆ·SO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
        } else {
            if (player.HasKitchenObject()) {

            } else {
                GetKitchenObject().SetkitchenObjectParent(player);
            }
        }
    }

    
}
 