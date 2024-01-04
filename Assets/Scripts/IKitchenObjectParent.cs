using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent {

    // Attach a kitchen object to this parent.
    public void SetKitchenObject(KitchenObject kitchenObject);

    // Get the kitchen object attached to this parent.
    public KitchenObject GetKitchenObject();

    // Remove the kitchen object from this parent.
    public void ClearKitchObject();

    // Whether this parent has a kitchen object.
    public bool HasKitchenObject();

    // The location to place the kitchen object at this parent.
    public Transform GetKitchenObjectFollowTransform();
}
