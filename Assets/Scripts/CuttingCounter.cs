using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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

    public override void InteractAlternate(Player player) {
        base.InteractAlternate(player);
        if (HasKitchenObject()) { 
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}