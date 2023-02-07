using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter :BaseCounter,IKitchenObjectParent {
    // ³ø·¿ÎïÆ·SO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, GetKitchenObjectFollowTransform());
            kitchenObjectTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(this);
        } else {
            // Debug.Log(GetKitchenObject().GetkitchenObjectParent());
            GetKitchenObject().SetkitchenObjectParent(player);
        }
    }

    
}
 