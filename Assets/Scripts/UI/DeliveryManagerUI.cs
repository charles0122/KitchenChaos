using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        // 销毁容器内除菜谱模板以外的子物体
        foreach (Transform child in container) {
            if (child== recipeTemplate) {
                continue;
            }
            Destroy(child.gameObject);
        }
        // 生成菜谱并激活
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetRecipeSOList()) {
            Transform recipeTransform =  Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<RecipeSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}