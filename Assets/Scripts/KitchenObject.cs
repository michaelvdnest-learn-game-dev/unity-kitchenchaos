using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }

    public void SetClearCounter(ClearCounter clearCounter) {

        // Remove from the previous counter
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchObject();
        }

        if (clearCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has a KitchObject!");
        }

        // Add to the new counter
        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);

        // Show the kitchen object on the counter
        transform.parent = clearCounter.GetCounterTopPoint();
        transform.localPosition = Vector3.zero;
    }
}
