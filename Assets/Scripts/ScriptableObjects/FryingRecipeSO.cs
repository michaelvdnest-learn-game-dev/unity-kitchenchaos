using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject {
    // The uncut input kitchen object
    public KitchenObjectSO input;

    // The cut output kitchen object
    public KitchenObjectSO output;

    // The maximum time to fry the kitchen object
    public float fryingTimerMax;
}
