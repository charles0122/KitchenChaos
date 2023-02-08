using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    // ����
    public static DeliveryManager Instance { get; private set; }

    // �����б�
    [SerializeField] private RecipeListSO recipeListSO;

    // �Ⱥ���׶���
    private List<RecipeSO> waitingRecipeSOList;
    // ��󶩵���
    private int waitingRecipeMax = 4;

    // ���ɵȺ������ʱ
    private float spawnRecipeTimer;
    // ���ɵȺ������ʱ��
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
                RecipeSO recipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(recipeSO.recipeName);
                waitingRecipeSOList.Add(recipeSO);
            }
        }
    }


    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {
        for(int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO wartingRecipeSO = waitingRecipeSOList[i];
            if (wartingRecipeSO.kitchenObjectSOList.Count==plateKitchenObject.GetKitchenObjectSOList().Count) {
                // ���Ⱥ򶩵��в��׵�ԭ�������������е�ԭ���������ʱ
                // ���������Ҫ��ԭ�������ﶼ��
                bool plateContentsMatchRecipe = true;
                // �����Ⱥ򶩵��еĲ���ԭ��
                foreach (KitchenObjectSO kitchenObjectSO in wartingRecipeSO.kitchenObjectSOList) {
                    // ����������ȱ�ٲ�������ԭ��
                    bool ingredientFound = false;
                    // ���������е�ԭ��
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (plateKitchenObjectSO==kitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // ������û���ҵ�������Ҫ��ԭ��
                        plateContentsMatchRecipe = false;
                    }
                }
                if (plateContentsMatchRecipe) {
                    // ��������ȷ�Ĳ���
                    return;
                }
            }
        }
        // ���û�������ȷ�Ĳ���
    }

}
