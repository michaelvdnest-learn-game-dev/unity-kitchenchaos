using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnObjectPlaced;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player) {
        Debug.Log("BaseCounter.InteractAlternate();");
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return (kitchenObject != null);
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }
}
