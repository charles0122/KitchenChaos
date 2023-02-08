using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StoveCounter : BaseCounter,IHasProgress {
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    // ״̬��
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;    

    // ��ըʱ��
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    // �ջ�ʱ��
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    // ״̬�ı��¼�
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs:EventArgs {
        public State state;
    }
    // �������¼�
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


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
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
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
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state= state });
                       
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        // ���ɼ�ը�����Ʒ
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        // �л�״̬
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
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
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                });
            }
        } else {
            if (player.HasKitchenObject()) {
                // ��������������Ʒ
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // ���Ի�ȡ�ֳ����� ���û���ֳ����� plateKitchenObject = null
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        // ���Խ������ϵ���Ʒ�ŵ�������

                        // ���ٹ�̨�е���Ʒ
                        GetKitchenObject().DestroySelf();
                        // ����״̬
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                } else {
                    // ����ֳֳ������������Ʒ
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // ��̨�Ϸ�������
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            // ���������������ԭ��

                            // ����������е���Ʒ
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                // ���в�̨����Ʒ�Ż��������
                GetKitchenObject().SetkitchenObjectParent(player);
                // ����״̬
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
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