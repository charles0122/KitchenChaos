using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    // [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                // ������Ż�û�е���Ʒ �ŵ��в�̨��
                player.GetKitchenObject().SetkitchenObjectParent(this);
                cuttingProgress = 0;
            }
        } else {
            if (player.HasKitchenObject()) {

            } else {
                // ���в�̨����Ʒ�Ż��������
                GetKitchenObject().SetkitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        base.InteractAlternate(player);
        if (HasKitchenObject()) {
            // �в˽���
            cuttingProgress++;
            // �ҵ��в˲���
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (cuttingProgress>=cuttingRecipeSO.cuttingProgressMax) {
                // ���ݲ����ҵ�������Ʒ
                KitchenObjectSO output = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                // ��������
                GetKitchenObject().DestroySelf();
                // �����в���Ʒ
                KitchenObject.SpawnKitchenObject(output, this);
            }


            ;
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null ;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}