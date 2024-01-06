using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
    public override void Interact(Player player) {
        // Drop items on the counter
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                // Set the kitchen object on the player to this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
