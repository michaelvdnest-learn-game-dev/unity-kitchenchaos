using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private float spawnPlateTimerMax;

    [SerializeField] private float spawnPlateAmountMax;

    private float spawnPlateTimer;
    private float spawnPlateAmount;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax ) {
            spawnPlateTimer = 0f;

            if (spawnPlateAmount < spawnPlateAmountMax ) {
                spawnPlateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (spawnPlateAmount > 0) {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                spawnPlateAmount--;
            }
        }
    }
}
