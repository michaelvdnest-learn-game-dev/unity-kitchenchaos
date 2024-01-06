using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    // Array of objects that can be cooked on this counter
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    // Array of objects that can be burned on this counter
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State state;

    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        SetState(State.Idle);
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                        SetState(State.Fried);
                        burningTimer = 0f;
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        SetState(State.Burned);

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
        // Drop items on the counter
        if (!HasKitchenObject()) {
            if (CanFryKitchenObject(player)) {
                // Set the kitchen object on the player to this counter only if it can be cut
                player.GetKitchenObject().SetKitchenObjectParent(this);

                // Store the recipe
                fryingRecipeSO = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());

                SetState(State.Frying);
                fryingTimer = 0f;
            }
        }
        else {
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
                SetState(State.Idle);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    private void SetState(State state) {
        Debug.Log(state);
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            state = this.state
        });
    }

    private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO input) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == input) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO input) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == input) {
                return burningRecipeSO;
            }
        }
        return null;
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSO(input);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        }
        return null;
    }

    public bool CanFryKitchenObject(IKitchenObjectParent parent) {
        if (parent.HasKitchenObject()) {
            KitchenObjectSO kitchenObjectSO = parent.GetKitchenObject().GetKitchenObjectSO();
            if (GetOutputForInput(kitchenObjectSO) != null) {
                return true;
            }
        }
        return false;
    }
}
