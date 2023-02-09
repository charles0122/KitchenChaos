using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RecipeSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconsContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeName.text = recipeSO.recipeName;
        // 销毁容器下除模板以外的图片
        foreach (Transform child in iconsContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO child in recipeSO.kitchenObjectSOList) {
            Transform iconTransform = Instantiate(iconTemplate, iconsContainer);
            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<Image>().sprite = child.sprite;
        }
    }
}   