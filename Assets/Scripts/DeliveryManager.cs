using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using static UnityEngine.ParticleSystem;

public class DeliveryManager : MonoBehaviour {

    public static DeliveryManager Instance {  get; private set; }

    // The list of recipes that can be made by this manager
    [SerializeField] private RecipeListSO recipeListSO;

    [SerializeField] private float spawnRecipeTimerMax;
    [SerializeField] private int waitingRecipesMax;

    // Contains a list of recipes that are waiting to be completed
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log(waitingRecipeSOList.Count);
                Debug.Log(waitingRecipeSO.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {

        // deliver the plate if it is in the list of waiting list of recipes
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSOList) {
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // a recipe and the plate has the same number of ingredients
                if (waitingRecipeSO.kitchenObjectSOList.ToHashSet().SetEquals(plateKitchenObject.GetKitchenObjectSOList())) {
                    // the recipe and the plate have the same ingredients
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    plateKitchenObject.DestroySelf();
                    Debug.Log("delivered: " + waitingRecipeSO.recipeName);
                    Debug.Log(waitingRecipeSOList.Count);
                    return;
                }
            }
        }
    }
}
