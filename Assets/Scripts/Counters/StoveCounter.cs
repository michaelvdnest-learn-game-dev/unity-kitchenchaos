using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter {

    // Array of objects that can be cooked on this counter
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    // Array of objects that can be burned on this counter
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private enum State {
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
                        Debug.Log("Fried!");
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
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

                state = State.Frying;
                fryingTimer = 0f;
            }
        }
        else {
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
            }
        }
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
