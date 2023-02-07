using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {

    // ³ø·¿ÎïÆ·SO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void  Interact(Player player) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(player);
    }

    
}