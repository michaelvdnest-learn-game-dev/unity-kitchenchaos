using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject {
    // The uncut input kitchen object
    public KitchenObjectSO input;

    // The cut output kitchen object
    public KitchenObjectSO output;

    // The maximum time after which the kitchen object will be burned.
    public float burningTimerMax;
}
