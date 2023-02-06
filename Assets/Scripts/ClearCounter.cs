using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour {
    // ������ƷSO
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // ��̨���� ������ƷSO ��λ��
    [SerializeField] private Transform counterTopPoint;
    // ������Ʒ
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
 