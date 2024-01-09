using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        // Hide the template
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual();
    }
    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        // Remove all templates expect the recipeTemplate
        foreach (Transform child in container) {
            if (child != recipeTemplate) {
                Destroy(child.gameObject);
            }
        }

        // Cycle through all the waiting recipes and show them
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            // Spawn the recipe template inside the container
            Transform recipeTransform =  Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleRecipeUI>().SetRecipeSO(recipeSO);
        }
    }

}
