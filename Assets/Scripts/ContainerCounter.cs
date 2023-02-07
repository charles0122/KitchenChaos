using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {

    // ������ƷSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void  Interact(Player player) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(player);
    }

    
}