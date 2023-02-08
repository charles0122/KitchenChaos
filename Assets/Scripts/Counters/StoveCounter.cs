using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StoveCounter : BaseCounter {
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    // 状态机
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;    

    // 煎炸时间
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    // 烧毁时间
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    // 状态改变事件
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs:EventArgs {
        public State state;
    }

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch (state) { 
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        GetKitchenObject().DestroySelf();
                        // 生成煎炸结果物品
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        Debug.Log(GetKitchenObject().GetKitchenObjectSO());
                        // 获取煎炸超时食谱
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0f;
                        // 切换状态
                        state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state= state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        // 生成煎炸结果物品
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        // 切换状态
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                // 玩家拿着还没切的物品 放到切菜台上
                player.GetKitchenObject().SetkitchenObjectParent(this);

                fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                fryingTimer = 0f;
                state = State.Frying;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
            }
        } else {
            if (player.HasKitchenObject()) {

            } else {
                // 将切菜台的物品放回玩家手中
                GetKitchenObject().SetkitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
            }
        }
    }

    

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
        return fryingRecipeSO != null ? fryingRecipeSO.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == input) {
                return fryingRecipeSO;
            }
        }
        return null;
    }


    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == input) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}