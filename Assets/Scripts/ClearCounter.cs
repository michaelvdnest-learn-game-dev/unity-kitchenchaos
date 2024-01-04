using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update() {
        if (testing && Input.GetKeyUp(KeyCode.T)) {
            if (kitchenObject != null) {
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }
    public void Interact() {
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            Debug.Log(kitchenObject.GetKitchenObjectParent());
        }
    }
   
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
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
