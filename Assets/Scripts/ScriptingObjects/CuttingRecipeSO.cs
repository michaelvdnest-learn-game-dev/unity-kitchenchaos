using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject {
    // The uncut input kitchen object
    public KitchenObjectSO input;

    // The cut output kitchen object
    public KitchenObjectSO output;

    // The maximum amount of times to cut the kitchen object before it is cut
    public int cuttingProgressMax;
}
