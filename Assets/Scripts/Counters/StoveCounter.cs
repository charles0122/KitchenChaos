using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter {
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    // ״̬��
    private enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;    

    // ��ըʱ��
    private float fryingTimer;
    // �ջ�ʱ��
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
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
                        // ���ɼ�ը�����Ʒ
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        Debug.Log(GetKitchenObject().GetKitchenObjectSO());
                        // ��ȡ��ը��ʱʳ��
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0f;
                        // �л�״̬
                        state = State.Fried;
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        // ���ɼ�ը�����Ʒ
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        // �л�״̬
                        state = State.Burned;
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
                // ������Ż�û�е���Ʒ �ŵ��в�̨��
                player.GetKitchenObject().SetkitchenObjectParent(this);

                fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                fryingTimer = 0f;
                state = State.Frying;
            }
        } else {
            if (player.HasKitchenObject()) {

            } else {
                // ���в�̨����Ʒ�Ż��������
                GetKitchenObject().SetkitchenObjectParent(player);
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