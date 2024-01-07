using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {

        // Remove from the previous parent
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchObject();
        }

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("IKitchenObjectParent already has a KitchObject!");
        }

        // Add to the new parent
        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);

        // Show the kitchen object on the parent
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchObject();
        Destroy(gameObject);
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = (PlateKitchenObject)this;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }
}
