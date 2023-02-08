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
                // ��������������Ʒ
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // ���Ի�ȡ�ֳ����� ���û���ֳ����� plateKitchenObject = null
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // ���Խ������ϵ���Ʒ�ŵ�������

                        // ���ٹ�̨�е���Ʒ
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // ����ֳֳ������������Ʒ
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // ��̨�Ϸ�������
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            // ���������������ԭ��

                            // ����������е���Ʒ
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
 