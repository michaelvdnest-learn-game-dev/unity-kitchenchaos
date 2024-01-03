using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObject;
    [SerializeField] private Transform counterTopPoint;

    public void Interact() {
        Debug.Log(this);
        Transform kitchenObjectTransform = Instantiate(kitchenObject.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        KitchenObjectSO kitchenObjectSO = kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO();
        Debug.Log(kitchenObjectSO.objectName);
    }
}
