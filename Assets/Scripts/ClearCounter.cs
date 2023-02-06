using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour {
    // 厨房物品SO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // 柜台生成 厨房物品SO 的位置
    [SerializeField] private Transform counterTopPoint;
    // 厨房物品
    private KitchenObject kitchenObject;

    public void Interact() {
        if (kitchenObject==null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
        } else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }
}
 