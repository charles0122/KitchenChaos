using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CuttingCounter : BaseCounter,IHasProgress {
    // [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    // �в˽�����
    private int cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
   

    // �Ӿ�Ч���¼�
    public event EventHandler OnCut;
    // �в��¼�
    public static event EventHandler OnAnyCut;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                // ������Ż�û�е���Ʒ �ŵ��в�̨��
                player.GetKitchenObject().SetkitchenObjectParent(this);
                cuttingProgress = 0;

                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
                
            }
        } else {
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

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            // �ҵ��в˲���
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

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