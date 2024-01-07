using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        // Drop items on the counter
        if (!HasKitchenObject()) {
            // Counter does not have an object on it
            if (player.HasKitchenObject()) {
                // Set the kitchen object on the player to this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // Counter has an object on it.
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is holding a plate, put the item on the plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }

                }
            } else {
                // Move the item to the player
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
