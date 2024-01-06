using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlayerGrabbedObject += PlatesCounter_OnPlayerGrabbedObject;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform  = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count , 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
        if (plateVisualGameObjectList.Count > 0) {
            GameObject plateVisual = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
            plateVisualGameObjectList.Remove(plateVisual);
            Destroy(plateVisual);
        }
    }
}
