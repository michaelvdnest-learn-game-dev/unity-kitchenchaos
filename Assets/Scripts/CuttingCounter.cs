using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        // Drop items on the counter
        if (!HasKitchenObject()) {
            if (CanCutKitchenObject(player)) {
                // Set the kitchen object on the player to this counter only if it can be cut
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (CanCutKitchenObject(this)) {
            KitchenObjectSO cutKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO.output;
            }
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
