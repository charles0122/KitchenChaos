using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    // 单例
    public static DeliveryManager Instance { get; private set; }

    // 菜谱生成事件
    public event EventHandler OnRecipeSpawned;
    
    // 菜谱完成事件
    public event EventHandler OnRecipeCompleted;
    // 菜谱结果事件
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed;

    // 菜谱列表
    [SerializeField] private RecipeListSO recipeListSO;

    // 等候菜谱订单
    private List<RecipeSO> waitingRecipeSOList;
    // 最大订单数
    private int waitingRecipeMax = 4;

    // 生成等侯订单倒计时
    private float spawnRecipeTimer;
    // 生成等侯订单所需时间
    private float spawnRecipeTimerMax = 4f;


    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count<waitingRecipeMax) {
                RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(recipeSO.recipeName);
                // 生成的菜谱放入等待订单
                waitingRecipeSOList.Add(recipeSO);
 
                OnRecipeSpawned.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {
        for(int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO wartingRecipeSO = waitingRecipeSOList[i];
            if (wartingRecipeSO.kitchenObjectSOList.Count==plateKitchenObject.GetKitchenObjectSOList().Count) {
                // 当等候订单中菜谱的原料数量和盘子中的原料数量相等时
                // 假设菜谱需要的原料盘子里都有
                bool plateContentsMatchRecipe = true;
                // 遍历等候订单中的菜谱原料
                foreach (KitchenObjectSO kitchenObjectSO in wartingRecipeSO.kitchenObjectSOList) {
                    // 假设盘子中缺少菜谱所需原料
                    bool ingredientFound = false;
                    // 遍历盘子中的原料
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (plateKitchenObjectSO==kitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // 盘子上没有找到菜谱需要的原料
                        plateContentsMatchRecipe = false;
                    }
                }
                if (plateContentsMatchRecipe) {
                    // 玩家完成正确的菜谱
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // 玩家没有完成正确的菜谱
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }


    public List<RecipeSO> GetRecipeSOList() {
        return waitingRecipeSOList;
    }

}
