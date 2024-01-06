using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CuttingCounter : BaseCounter {


    // Raised when current progess changes
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    // Array of objects that can be cut on this counter
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    // Store how many cuts have been made on the kitchen object
    private int cuttingProgress;

    public override void Interact(Player player) {
        // Drop items on the counter
        if (!HasKitchenObject()) {
            if (CanCutKitchenObject(player)) {
                // Set the kitchen object on the player to this counter only if it can be cut
                player.GetKitchenObject().SetKitchenObjectParent(this);

                // Cutting progress is 0 when an object is placed on the counter
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                    progressNormalized = 0f,
                });
            }
        } else {
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (CanCutKitchenObject(this)) {

            KitchenObjectSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(kitchenObjectSO);

            OnCut?.Invoke(this, EventArgs.Empty);

            cuttingProgress += 1;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(kitchenObjectSO);
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            }
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(input);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;
    }



    public bool CanCutKitchenObject(IKitchenObjectParent parent) {
        if (parent.HasKitchenObject()) {
            KitchenObjectSO kitchenObjectSO = parent.GetKitchenObject().GetKitchenObjectSO();
            if (GetOutputForInput(kitchenObjectSO) != null) {
                return true;
            }
        }
        return false;

    }


}