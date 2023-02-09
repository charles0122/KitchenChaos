using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CuttingCounter : BaseCounter,IHasProgress {
    // [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    // 切菜进度条
    private int cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
   

    // 视觉效果事件
    public event EventHandler OnCut;
    // 切菜事件
    public static event EventHandler OnAnyCut;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                // 玩家拿着还没切的物品 放到切菜台上
                player.GetKitchenObject().SetkitchenObjectParent(this);
                cuttingProgress = 0;

                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
                
            }
        } else {
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
                // 将切菜台的物品放回玩家手中
                GetKitchenObject().SetkitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        base.InteractAlternate(player);
        if (HasKitchenObject()) {
            // 切菜进度
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            // 找到切菜菜谱
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress>=cuttingRecipeSO.cuttingProgressMax) {
                // 根据菜谱找到生成物品
                KitchenObjectSO output = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                // 自我销毁
                GetKitchenObject().DestroySelf();
                // 生成切菜物品
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