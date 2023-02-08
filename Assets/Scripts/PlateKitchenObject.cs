using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    // ��Ч��ԭ�ϱ� ���Է���������
    [SerializeField] private List<KitchenObjectSO> vaildKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!vaildKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // ��ǰԭ�ϲ�����Чԭ��
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // ��ǰԭ���б��Ѿ��и�ԭ����
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }
}