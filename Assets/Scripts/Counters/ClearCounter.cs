using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter :BaseCounter {
    // ������ƷSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
        } else {
            // ��������ж���
            if (player.HasKitchenObject()) {
                // ��� ������ϵ���Ʒ�� PlateKitchenObject ���͵�
                if (player.GetKitchenObject() is PlateKitchenObject) {
                    // �ֳ�����
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    // ���Խ������ϵ���Ʒ�ŵ�������
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
 