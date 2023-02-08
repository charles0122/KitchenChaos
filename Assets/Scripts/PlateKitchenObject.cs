using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject {

    // ��Ч��ԭ�ϱ� ���Է���������
    [SerializeField] private List<KitchenObjectSO> vaildKitchenObjectSOList;
    // ��ǰ�����ϵ�ԭ��
    private List<KitchenObjectSO> kitchenObjectSOList;


    // ԭ������¼�
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

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
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }
}